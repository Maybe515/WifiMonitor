using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WifiMonitor
{
    public partial class Form1 : Form
    {
        const string NetshPath = @"C:\Windows\System32\netsh.exe";
        private bool isMonitoring = true;

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            StartMonitoring();
        }

        private void InitializeForm()
        {
            listView1.ColumnClick += listView1_ColumnClick;
            listView1.ListViewItemSorter = new ListViewColumnSorter();
        }

        /// <summary>モニタリングを開始する（ループ処理）</summary>
        private void StartMonitoring()
        {
            var ps = new ProcessStartInfo(NetshPath)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var uiContext = SynchronizationContext.Current;

            Task.Run(async () =>                    // 非同期処理
            {
                while (isMonitoring)
                {
                    try
                    {
                        await Task.Delay(1000);     // 1秒間隔で情報を取得

                        var wlanInterface = GetWlanInterface(ps);
                        var wlanInfo = GetWlanInfo(ps).Where(x => x != null).ToArray();

                        uiContext.Post(_ => UpdateListView(wlanInfo, wlanInterface[0]), null);
                        uiContext.Post(_ => UpdateStatusLabel(wlanInfo.Length, GetNowTime(), wlanInterface[1]), null);
                        uiContext.Post(_ => ToggleLamp(), null);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[Monitoring Loop] Error: {ex.Message}");
                    }
                }
            });
        }

        /// <summary>Wi-Fi情報を取得する</summary>
        /// <param name="ps">コマンドプロセス</param>
        /// <returns>Wi-Fi情報{ SSID, チャネル, シグナル, 認証, MAC, 暗号化, 無線タイプ, ネットワークの種類 }</returns>
        private string[] GetWlanInfo(ProcessStartInfo ps)
        {
            string[] patterns = { "^SSID", "ネットワークの種類:", "認証:", "暗号化:", "BSSID1:", "シグナル:", "無線タイプ:", "チャネル:" };
            int[] displayOrder = { 0, 7, 5, 2, 4, 3, 6, 1 };    // 表示する順番（index）
            ps.Arguments = "wl sh networks mode=bssid";

            try
            {
                string output = "";
                using (Process process = Process.Start(ps))
                {
                    output = FormatOutput(process.StandardOutput.ReadToEnd());
                }
                var lines = output.Split('\n');
                int apCount = GetAccessPointCount(lines);
                var result = new string[apCount];

                int i = 0, j = 0;
                string[] fields = new string[8];

                foreach (string line in lines)
                {
                    if (Regex.IsMatch(line, patterns[i]))
                    {
                        string[] parts = line.Split(new[] { ':' }, 2);
                        fields[i] = !string.IsNullOrWhiteSpace(parts[1]) ? parts[1] : "***";
                        i++;

                        if (i == patterns.Length)
                        {
                            string[] recorded = displayOrder.Select(index =>fields[index]).ToArray();
                            result[j++] = string.Join(",", recorded);
                            i = 0;
                            fields = new string[8];
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[GetWlanInfo] Error: {ex.Message}");
                return Array.Empty<string>();
            }
        }

        /// <summary>接続しているSSIDの情報を取得する</summary>
        /// <param name="ps">コマンドプロセス</param>
        /// <returns>{ SSID, 受信速度 }</returns>
        private string[] GetWlanInterface(ProcessStartInfo ps)
        {
            string[] result = new string[2];
            string[] patterns = { "^SSID", "受信速度" };
            ps.Arguments = "wl sh interface";

            try
            {
                string output = "";
                using (Process process = Process.Start(ps))
                {
                    output = FormatOutput(process.StandardOutput.ReadToEnd());
                }
                var lines = output.Split('\n');

                for (int i = 0; i < patterns.Length; i++)
                {
                    foreach (string line in lines)
                    {
                        if (Regex.IsMatch(line, patterns[i]))
                        {
                            string[] parts = line.Split(':');
                            result[i] = parts.Length > 1 ? parts[1] : "***";
                            break;
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[GetWlanInterface] Error: {ex.Message}");
                return new[] { "***", "0" };
            }
        }

        /// <summary>受信したアクセスポイント数を取得</summary>
        /// <param name="lines">コマンドプロセスの出力結果</param>
        /// <returns>アクセスポイント数</returns>
        private int GetAccessPointCount(string[] lines)
        {
            foreach (string line in lines)
            {
                if (line.Contains("現在") && line.Contains("の"))
                {
                    var parts = line.Split(new[] { "現在", "の" }, StringSplitOptions.None);
                    if (parts.Length > 1 && int.TryParse(parts[1], out int count))
                        return count;
                }
            }
            return 0;
        }

        /// <summary>現在日時を取得する</summary>
        /// <returns>現在日時（yyyy/MM/dd HH:mm:ss）</returns>
        private string GetNowTime() => DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        /// <summary>ステータスラベルを更新する</summary>
        /// <param name="apCount">アクセスポイント数</param>
        /// <param name="timestamp">現在日時</param>
        /// <param name="speed">接続しているWi-Fiの受信速度</param>
        private void UpdateStatusLabel(int apCount, string timestamp, string speed)
        {
            toolStripStatusLabel1.Text = $"AP：{apCount}";
            toolStripStatusLabel2.Text = $"LastRead：{timestamp}";
            toolStripStatusLabel3.Text = $"速度：{speed} Mbps";
        }

        /// <summary>稼働中ランプを点滅させる</summary>
        private void ToggleLamp()
        {
            label1.ForeColor = label1.ForeColor == Color.Red ? Color.Gray : Color.Red;
        }

        /// <summary>コマンドプロセスの出力結果を整形する</summary>
        /// <param name="output">プロセス出力結果</param>
        /// <returns>プロセス出力結果（整形済み）</returns>
        private static string FormatOutput(string output)
        {
            try
            {
                return output
                    .Replace("\r\n", "\n")   // Windows改行 → Unix改行
                    .Replace("\r", "")       // 残ったCRを除去
                    .Replace(" ", "")        // 文字間の空白を除去
                    .Trim();                 // 前後の空白を除去
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[FormatOutput] Error: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>抽出した情報をリストビューに反映させる</summary>
        /// <param name="wlanInfo">Wi-Fi情報</param>
        /// <param name="currentSSID">接続しているSSID</param>
        private void UpdateListView(string[] wlanInfo, string currentSSID)
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();

            foreach (var info in wlanInfo)
            {
                var fields = info.Split(',');
                var item = new ListViewItem(fields[0]);

                if (fields[0] == currentSSID)
                {
                    item.Font = new Font(listView1.Font, FontStyle.Bold);
                    item.BackColor = Color.LightGray;
                }

                for (int i = 1; i < fields.Length; i++)
                    item.SubItems.Add(fields[i]);

                listView1.Items.Add(item);
            }

            listView1.EndUpdate();
        }

        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        /// <summary>電波取得の一時停止・再開を切り替える</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 電波取得一時停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isMonitoring = !isMonitoring;
            Text = isMonitoring ? "WifiMonitor" : "WifiMonitor（停止中）";
            label1.ForeColor = isMonitoring ? Color.Gray : Color.Red;

            if (isMonitoring)
                StartMonitoring();
        }

        /// <summary>カラムをクリックしたときに並べ替え処理をする</summary>
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var sorter = listView1.ListViewItemSorter as ListViewColumnSorter;

            if (e.Column == sorter.SortColumn)
                sorter.Order = sorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            else
            {
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }

            listView1.Sort();
        }

        private void 小ToolStripMenuItem_Click(object sender, EventArgs e) => listView1.Font = new Font("メイリオ", 8);
        private void 標準ToolStripMenuItem_Click(object sender, EventArgs e) => listView1.Font = new Font("メイリオ", 11);
        private void 大ToolStripMenuItem_Click(object sender, EventArgs e) => listView1.Font = new Font("メイリオ", 15);
        private void 特大ToolStripMenuItem_Click(object sender, EventArgs e) => listView1.Font = new Font("メイリオ", 19);
    }

    class ListViewColumnSorter : IComparer
    {
        public int SortColumn { get; set; } = 0;
        public SortOrder Order { get; set; } = SortOrder.Ascending;

        public int Compare(object x, object y)
        {
            var item1 = x as ListViewItem;
            var item2 = y as ListViewItem;

            string text1 = item1?.SubItems[SortColumn].Text ?? "";
            string text2 = item2?.SubItems[SortColumn].Text ?? "";

            int result = string.Compare(text1, text2);
            return Order == SortOrder.Descending ? -result : result;    // 昇順と降順を反転
        }
    }
}
