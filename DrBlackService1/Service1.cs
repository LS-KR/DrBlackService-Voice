using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace DrBlackService1
{
    public partial class DrBlackService1 : ServiceBase
    {
        bool issound;
        bool isbir = false;
        public DrBlackService1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 启动时执行
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            DateTime dateTime = DateTime.Now;
            string date = "";
            string fdate = "";
            string log = "";
            const string fPath = "C:\\IDS\\Dev\\log.log";
            string sPath = "C:\\IDS\\Dev";
            //bool issound;
            if (!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);
            if (!File.Exists(fPath))
            {
                Syscmd.ExecuteCMD("echo Created By lskr.Do not use without authorization.=)>> " + fPath, 0);
                Syscmd.ExecuteCMD("echo 余音歆风によって作成されました。無断で使用しないでください。=)>> " + fPath, 0);
                Syscmd.ExecuteCMD("echo [" + dateTime.ToString() + "]\tFile Created. >> " + fPath, 0);
            }
            else
            {
                File.AppendAllText((fPath), ("[" + dateTime.ToString() + "]\tService Start.\n"));
            }
            if (File.Exists("C:\\IDS\\SR.txt"))
            {
                fdate = File.ReadAllLines("C:\\IDS\\SR.txt")[0];
                File.AppendAllText(fPath, "[" + dateTime.ToString() + "]\tSR.txt detected.\n");
            }
            //检测是否激活语音
            date = Convert.ToString(dateTime.Month) + "-" + Convert.ToString(dateTime.Day);
            if (File.Exists("C:\\IDS\\Dev\\Active.ids"))
            {
                if (File.ReadAllText("C:\\IDS\\Dev\\Active.ids").Contains("\a")) 
                    issound = true;
                else
                    issound = false;
            }
            else
            {
                if (File.Exists("C:\\IDS\\token.txt"))
                {
                    string tok = File.ReadAllText("C:\\IDS\\token.txt");
                    if ((ServRequst("{\"post_id\":19854}", tok, "{\"post_id\":19854}")) != null)
                        issound = true;
                    else
                        issound = false;
                }
                else
                    issound = false;
                if (issound)
                    Syscmd.ExecuteCMD("echo \a >> C:\\IDS\\Dev\\Active.ids", 10);
            }
            File.AppendAllText(fPath, "[" + dateTime.ToString() + "]\tissound=\t" + Convert.ToString(issound) + "\n");
            if (fdate.Contains(date))
            {
                File.AppendAllText(fPath, "[" + dateTime + "]\tThe date matches.\n");
            }
            if ((fdate.Contains(date)) && issound && (fdate.Length == date.Length))
            {
                Media.PlaySound("C:\\IDS\\Dev\\media\\bthsolo.wav", IntPtr.Zero, Media.SND_ASYNC | Media.SND_NODEFAULT);
                File.AppendAllText(fPath, "[" + dateTime + "]\tThe date matches, the API has been called to play \'C:\\IDS\\Dev\\media\\bthsolo.wav\'.\n");
                isbir = true;
            }
            Task.Factory.StartNew(Handle);
        }
        protected override void OnStop()
        {
            string sPath = "C:\\IDS\\Dev";
            DateTime datetime = DateTime.Now;
            File.AppendAllText((sPath + "\\log.log"), ("[" + datetime.ToString() + "]\tService Stop.\n"));
        }
        /// <summary>
        /// 轮询执行
        /// </summary>
        private void Handle()
        {
            DateTime datetime = DateTime.Now;
            string log = "";
            const string fPath = "C:\\IDS\\Dev\\log.log";
            string sPath = "C:\\IDS\\Dev";
            int dayn = datetime.Day;
            int dayl;
            //bool issound;
            //启动后先执行一次
            if (isbir)
                Thread.Sleep(4000);
            if (issound)
                Task.Factory.StartNew(playvoice);//开机语音
            dayl = dayn;
            //轮询执行，每10s执行一次
            while (true)
            {
                string date = "";
                string fdate = "";
                Thread.Sleep(30000);
                datetime = DateTime.Now;
                dayn = datetime.Day;
                if (!Directory.Exists(sPath))
                    Directory.CreateDirectory(sPath);
                if (!File.Exists(fPath))
                {
                    Syscmd.ExecuteCMD("echo [" + datetime.ToString() + "]\tFile Created. >> " + fPath, 0);
                }
                if (dayn != dayl)
                {
                    dayl = dayn;
                    if (File.Exists("C:\\IDS\\SR.txt"))
                    {
                        fdate = File.ReadAllLines("C:\\IDS\\SR.txt")[0];
                        File.AppendAllText(fPath, "[" + datetime.ToString() + "]\tSR.txt detected.\n");
                    }
                    date = Convert.ToString(datetime.Month) + "-" + Convert.ToString(datetime.Day);
                    if ((fdate.Contains(date)) && issound && (fdate.Length == date.Length))
                    {
                        Media.PlaySound("C:\\IDS\\Dev\\media\\bthsolo.wav", IntPtr.Zero, Media.SND_ASYNC | Media.SND_NODEFAULT);
                        File.AppendAllText(fPath, "[" + datetime + "]\tThe date matches, the API has been called to play \'C:\\IDS\\Dev\\media\\bthsolo.wav\'.\n");
                    }
                    log = "[" + datetime.ToString() + "]\tA new day, rechecking if the date matches the birthday...\n";
                    File.AppendAllText(fPath, log);
                }
                log = "[" + datetime.ToString() + "]\tService Time Tick.\n";
                File.AppendAllText(fPath, log);
            }
        }
        private void playvoice()
        {
            Voice_Achieve voice_Achieve = new Voice_Achieve();
            voice_Achieve.TimeAchieve();
            voice_Achieve.WeatherAchieve();
            voice_Achieve.FestivalAchieve();
            if (EMAth.GetRandom(1, 20) == 10)
                Media.PlaySound("C:\\IDS\\Dev\\media\\upd.wav", IntPtr.Zero, Media.SND_ASYNC | Media.SND_NODEFAULT);
        }
        private static string ServRequst(string RequestString, string token, string body)
        {
            string url = "";//这里因为涉及敏感数据,已被删除
            string ret = null;
            Encoding encoding = Encoding.UTF8;
            HttpWebResponse response;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "post";
            request.Headers.Add("Authorization", token);
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            /*using (var postStream = new StreamWriter(request.GetRequestStream()))
            {
                postStream.Write(body);
            }*/
            try
            {
                byte[] buffer = encoding.GetBytes(RequestString.ToString());
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    ret = reader.ReadToEnd();
                }
            }
            catch
            {
                ret = null;
            }
            return ret;
        }
    }
}
