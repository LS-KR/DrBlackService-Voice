using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DrBlackService1
{
    public partial class DrBlackService1 : ServiceBase
    {
        private partial class Voice_Achieve : Voice
        {
            public void FestivalAchieve()
            {
                int det = fgenerate();
                const string fPath = "C:\\IDS\\Dev\\log.log";
                if (!File.Exists(fPath))
                    File.Create(fPath);
                File.AppendAllText(fPath, "[" + DateTime.Now.ToString() + "]\tfestival=\t" + Convert.ToString(det) + "\n");
                switch (det)
                {
                    case (int)Festival.none:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\welc.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.new_year:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f01.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.lunar_eve:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f02.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.lunar_new:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f03.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.lunar_mid:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f04.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.chingming:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f05.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.dragonboat:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f06.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.labor:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f07.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.midautumn:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f08.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.national:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f09.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.chistmas:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f10.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.spring:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f11.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        Media.PlaySound("C:\\IDS\\Dev\\media\\d1.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.summer:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f12.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        Media.PlaySound("C:\\IDS\\Dev\\media\\d2.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.autumn:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f13.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        Media.PlaySound("C:\\IDS\\Dev\\media\\d3.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Festival.winter:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\f14.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        Media.PlaySound("C:\\IDS\\Dev\\media\\d4.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    default:
                        break;
                }
            }
            public void WeatherAchieve()
            {
                int det = wgenerate();
                const string fPath = "C:\\IDS\\Dev\\log.log";
                if (!File.Exists(fPath))
                    File.Create(fPath);
                File.AppendAllText(fPath, "[" + DateTime.Now.ToString() + "]\tweather=\t" + Convert.ToString(det) + "\n");
                switch (det)
                {
                    case (int)Weather.clear:
                        break;
                    case (int)Weather.sunny:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\w1.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Weather.cloudy:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\w2.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Weather.rainy:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\w3.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Weather.thunder:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\w4.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    default:
                        break;
                }
            }
            public void TimeAchieve()
            {
                int det = tgenerate();
                const string fPath = "C:\\IDS\\Dev\\log.log";
                if (!File.Exists(fPath))
                    File.Create(fPath);
                File.AppendAllText(fPath, "[" + DateTime.Now.ToString() + "]\ttimeing=\t" + Convert.ToString(det) + "\n");
                switch (det)
                {
                    case (int)Time.none:
                        break;
                    case (int)Time.wakeup:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\t1.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Time.morning:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\t2.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Time.noon:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\t3.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Time.afternoon:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\t4.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        if (EMAth.GetRandom(1, 10) == 5)
                            Media.PlaySound("C:\\IDS\\Dev\\media\\din.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Time.evening:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\t5.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    case (int)Time.midnight:
                        Media.PlaySound("C:\\IDS\\Dev\\media\\t6.wav", IntPtr.Zero, Media.SND_FILENAME | Media.SND_SYNC | Media.SND_NODEFAULT);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
