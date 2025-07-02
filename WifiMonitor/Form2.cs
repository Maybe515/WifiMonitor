/// 更新履歴
/// 2025.05.13　初版作成
/// 2025.07.02　[電波取得一時停止]ボタンを追加
/// 　　　　　　接続している無線の受信速度を表示

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WifiMonitor
{
    public partial class Form2 : Form
    {
        const string _netsh = @"C:\Windows\System32\netsh.exe";

        public Form2()
        {
            InitializeComponent();
            InitializeForm();
        }
        /// <summary>メインルーチン</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_Load(object sender, EventArgs e)
        {
            LoopDo();
        }
        /// <summary>フォームを初期化する</summary>
        private void InitializeForm()
        {            
            listView1.ColumnClick += new ColumnClickEventHandler(listView1_ColumnClick);    // カラムクリックイベントにハンドラを割り当て
            listView1.ListViewItemSorter = new ListViewColumnSorter();
        }
        /// <summary>ループ処理</summary>
        public void LoopDo()
        {
            ProcessStartInfo ps = new ProcessStartInfo(_netsh);
            ps.Arguments = " wl show networks mode=bssid";
            ps.CreateNoWindow = true;
            ps.UseShellExecute = false;
            ps.RedirectStandardOutput = true;
            ps.RedirectStandardError = true;

            bool Loop = true;
            SynchronizationContext mainContext = SynchronizationContext.Current;    // 画面に情報を反映させるため、メインスレッドの情報を保持

            // 非同期処理
            Task.Run(async () =>
            {
                while (Loop)
                {
                    try
                    {
                        await Task.Delay(1000);     // 1秒間隔で情報を取得

                        // リストビューに表示する情報を取得する
                        string[] result = GetWlanInfo(ps);
                        result = ArrResize(result);
                        mainContext.Post(x => AddListView(result), null);

                        // ラベルに表示する情報を取得する
                        string strDate = GetNowTime();
                        string strPing = GetRecieve();
                        AddLabel(result.Length, strDate, strPing);
                    }
                    catch (Exception) { }
                }
            });
        }
        /// <summary>アクセスポイント数を取得する</summary>
        /// <param name="ss">コマンドプロセスの出力結果</param>
        /// <returns>アクセスポイント数</returns>
        private int GetAP(string[] ss)
        {
            int result = new int();
            string ptn = "現在";
            string[] del = new string[] { ptn, "の" };

            try
            {
                foreach (string s in ss)
                {
                    if (Regex.IsMatch(s, ptn))
                    {
                        string[] splitCodes = s.Split(del, StringSplitOptions.None);
                        result = int.Parse(splitCodes[1]);
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>現在日時を取得する</summary>
        /// <returns>現在日時</returns>
        private string GetNowTime()
        {
            DateTime Now = DateTime.Now;
            string result = Now.ToString("yyyy/MM/dd HH:mm:ss");
            return result;
        }
        /// <summary>接続している無線の受信速度を取得する</summary>
        /// <returns>接続している無線の受信速度</returns>
        private string GetRecieve()
        {
            ProcessStartInfo ps = new ProcessStartInfo(_netsh);
            ps.Arguments = " wl show interface";
            ps.CreateNoWindow = true;
            ps.UseShellExecute = false;
            ps.RedirectStandardOutput = true;
            ps.RedirectStandardError = true;
        
            string ptn = "受信速度";
            string result = "";
        
            try
            {
                // コマンドを実行し、取得した出力結果を整形する
                Process p = Process.Start(ps);
                string output = p.StandardOutput.ReadToEnd();
                output = FormatOutput(output);
        
                string[] ss = output.Split('\n');
                foreach (string s in ss)
                {
                    if (Regex.IsMatch(s, ptn))
                    {
                        string[] splitCodes = s.Split(':');
                        result = splitCodes[1]; 
                    }
                }
                p.Close();
                p.Dispose();
        
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>取得した情報をリストビューに追加する</summary>
        /// <param name="result">WLAN情報</param>
        private void AddListView(string[] result)
        {   
            List<ListViewItem> listItem = new List<ListViewItem>();
            try
            { 
                for (int i = 0; i < result.Length; i++)
                {
                    string[] arrInfo = result[i].Split(',');
                    listItem.Add(new ListViewItem(arrInfo));
                }
                listView1.Items.Clear();
                listView1.Items.AddRange(listItem.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>取得した情報をラベルに表示する</summary>
        /// <param name="intAP">アクセスポイント数</param>
        /// <param name="strDate">現在日時</param>
        /// <param name="strPing">接続している無線の受信速度</param>
        private void AddLabel(int intAP, string strDate, string strPing)
        {
            toolStripStatusLabel1.Text = "AP：" + intAP;
            toolStripStatusLabel2.Text = "LastRead：" + strDate;
            toolStripStatusLabel3.Text = "速度：" + strPing + " Mbps";
        }
        /// <summary>WLAN情報を取得する</summary>
        /// <param name="ps">コマンドプロセス</param>
        /// <returns>{ SSID, チャネル, シグナル, 認証, MACアドレス, 暗号化, 無線タイプ, ネットワークの種類 }</returns>
        private string[] GetWlanInfo(ProcessStartInfo ps)
        {
            // 初期化
            int i = 0;
            int j = 0;
            string str = "";

            string ssid = "";
            string netwk = "";
            string sec = "";
            string enc = "";
            string mac = "";
            string sig = "";
            string wlty = "";
            string cha = "";

            string[] ptn = { "^SSID", "ネットワークの種類:", "認証:", "暗号化:", "BSSID1:", "シグナル:", "無線タイプ:", "チャネル:" };

            try
            {
                // コマンドを実行し、取得した出力結果を整形する
                Process p = Process.Start(ps);
                string output = p.StandardOutput.ReadToEnd();
                output = FormatOutput(output);

                string[] ss = output.Split('\n');
                int intAP = GetAP(ss);
                string[] result = new string[intAP];

                foreach (string s in ss)
                {
                    if (Regex.IsMatch(s, ptn[i]))
                    {
                        if (i == 0)   // SSID名
                        {
                            string[] splitCodes = s.Split(':');

                            if (splitCodes[1] != "")
                            {
                                ssid = splitCodes[1];
                            }
                            else
                            {
                                ssid = "***";
                            }
                            i++;
                        }
                        else
                        {
                            str = s.Replace(ptn[i], "");

                            switch (i)
                            {
                                case 1:     // ネットワークの種類
                                    netwk = str;
                                    break;

                                case 2:     // 認証
                                    sec = str;
                                    break;

                                case 3:     // 暗号化
                                    enc = str;
                                    break;

                                case 4:     // MACアドレス
                                    mac = str;
                                    break;

                                case 5:     // シグナル
                                    sig = str;
                                    break;

                                case 6:     // 無線タイプ
                                    wlty = str;
                                    break;

                                case 7:     // チャンネル
                                    cha = str;
                                    break;
                            }
                            i++;
                        }
                    }
                    if (i == ptn.Length)
                    {
                        string[] arr = { ssid, cha, sig, sec, mac, enc, wlty, netwk };
                        result[j] = string.Join(",", arr);
                        j++;

                        i = 0;
                    }
                }   
                p.Close();
                p.Dispose();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>コマンドプロセスの出力結果を整形する</summary>
        /// <param name="output">コマンドプロセスの出力結果</param>
        /// <returns>整形後の出力結果</returns>
        internal static string FormatOutput(string output)
        {
            try
            {
                string result = output.Replace("\r\r\n", "\n");     // 余分な改行や空白の除去
                result = result.Replace("\r", "");
                result = result.TrimEnd(' ');
                result = result.Replace(" ", "");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>配列に含まれている null を配列から除外する</summary>
        /// <param name="result">WLAN情報</param>
        /// <returns>リサイズ後の配列</returns>
        internal string[] ArrResize(string[] result)
        {
            int j = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != null)
                {
                    j++;
                }
            }
            Array.Resize(ref result, j);
            return result;
        }
        /// <summary>アプリケーションを終了する</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>リストビューのフォントサイズを小さくする</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Font = new Font("メイリオ", 8);
        }
        /// <summary>リストビューのフォントサイズを標準にする</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 標準ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Font = new Font("メイリオ", 11);
        }
        /// <summary>リストビューのフォントサイズを大きくする</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Font = new Font("メイリオ", 15);
        }
        /// <summary>リストビューのフォントサイズをより大きくする</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 特大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Font = new Font("メイリオ", 19);
        }
        /// <summary>カラムをクリックしたときに並べ替え処理をする</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var sorter = listView1.ListViewItemSorter as ListViewColumnSorter;

            // クリックされたカラムが現在ソートされているカラムと同じ場合は、ソート方向を反転させる
            if (e.Column == sorter.SortColumn)
            {
                sorter.Order = sorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // 新しいカラムでのソートを開始する場合は、昇順でソート
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }

            // ソートを実行
            listView1.Sort();
        }
    }
    /// <summary>カスタム Comparer クラス</summary>
    class ListViewColumnSorter : IComparer
    {
        public int SortColumn { get; set; }
        public SortOrder Order { get; set; }

        /// <summary>コンストラクタ</summary>
        public ListViewColumnSorter()
        {
            SortColumn = 0;
            Order = SortOrder.Ascending;
        }
        /// <summary>比較</summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            ListViewItem item1 = (ListViewItem)x;
            ListViewItem item2 = (ListViewItem)y;

            string text1 = item1.SubItems[SortColumn].Text;
            string text2 = item2.SubItems[SortColumn].Text;
       
            int result = String.Compare(text1, text2);  // 文字列として比較
     
            if (Order == SortOrder.Descending)      // 降順の場合は結果を反転
            {
                result = -result;
            }
            return result;
        }
    }
}
