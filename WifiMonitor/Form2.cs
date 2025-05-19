using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WifiMonitor
{
    public partial class Form2 : Form
    {
        const string _netsh = @"C:\Windows\System32\netsh.exe";

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeForm();
            LoopDo();
        }
        /// <summary>
        /// フォームを初期化する
        /// </summary>
        private void InitializeForm()
        {
            // string IcoPath = @"";
            // this.Icon = new System.Drawing.Icon(IcoPath);
            // this.AutoScroll = true;
        }
        /// <summary>
        /// ループ処理
        /// </summary>
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
                        result = arrResize(result);
                        mainContext.Post(x => AddListView(result), null);
                        
                        // ラベルに表示する情報を取得する
                        string strDate = GetNowTime();
                        AddLabel(result.Length, strDate);
                    }
                    catch (Exception) { }
                }
            });
        }
        /// <summary>
        /// アクセスポイント数を取得する
        /// </summary>
        private int GetAP(string[] ss)
        {
            int result = new int();
         
            try
            {
                string ptn = "現在";
                string[] del = new string[] { ptn, "の" };

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
        /// <summary>
        /// 現在日時を取得する
        /// </summary>
        private string GetNowTime()
        {
            DateTime Now = DateTime.Now;
            string result = Now.ToString("yyyy/MM/dd HH:mm:ss");
            return result;
        }
        /// <summary>
        /// 取得した情報をリストビューに追加する
        /// </summary>
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
        /// <summary>
        /// 取得した情報をラベルに表示する
        /// </summary>
        /// <param name="intAP"></param>
        /// <param name="strDate"></param>
        private void AddLabel(int intAP, string strDate)
        {
            toolStripStatusLabel1.Text = "AP数：" + intAP;
            toolStripStatusLabel2.Text = "LastRead：" + strDate;
        }
        /// <summary>
        /// WLAN情報を取得する
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="intAP"></param>
        /// <returns></returns>
        private string[] GetWlanInfo(ProcessStartInfo ps, int intAP)
        {
            // 初期化
            int i = 0;
            int j = 0;
            string str = "";

            string ssidNum = "";
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
                            ssidNum = splitCodes[0];
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
        /// <summary>
        /// プロセスの出力の整形
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        internal static string FormatOutput(string output)
        {
            try
            {
                string result = output.Replace("\r\r\n", "\n"); // 余分な改行や空白の除去
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
        /// <summary>
        /// 配列に含まれている null を配列から除外する
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        internal string[] arrResize(string[] result)
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
        /// <summary>
        /// アプリケーションを終了する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// リストビューのフォントサイズを小さくする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Font = new Font("メイリオ", 8);
        }
        /// <summary>
        /// リストビューのフォントサイズを標準にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 標準ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Font = new Font("メイリオ", 11);
        }
        /// <summary>
        /// リストビューのフォントサイズを大きくする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Font = new Font("メイリオ", 15);
        }
        /// <summary>
        /// リストビューのフォントサイズをより大きくする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 特大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Font = new Font("メイリオ", 19);
        }
    }
}
