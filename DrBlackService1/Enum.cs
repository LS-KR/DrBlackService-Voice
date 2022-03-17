using System;
using System.IO;
using System.ServiceProcess;
using System.Linq;

namespace DrBlackService1
{
    public partial class DrBlackService1 : ServiceBase
    {
        private class Voice
        {
            public enum Festival
            {
                none = 0,
                new_year = 1,
                lunar_eve = 2,
                lunar_new = 3,
                lunar_mid = 4,
                chingming = 5,
                dragonboat = 6,
                labor = 7,
                midautumn = 8,
                national = 9,
                chistmas = 10,
                spring = 11,
                summer = 12,
                autumn = 13,
                winter = 14
            }
            public enum Weather
            {
                clear = 0,
                sunny = 1,
                cloudy = 2,
                rainy = 3,
                thunder = 4
            }
            public enum Time
            {
                none = 0,
                wakeup = 1,
                morning = 2,
                noon = 3,
                afternoon = 4,
                evening = 5,
                midnight = 6
            }
            public bool iseastern()
            {
                return false;
            }
            /// <summary>
            /// 节日API获取生成
            /// </summary>
            /// <returns></returns>
            public static int fgenerate()
            {
                DateTime dt = DateTime.Now;
                Lunar.LunarDate ld = new Lunar.LunarDate();
                ld = Lunar.GetLunarDate(dt.Year, dt.Month, dt.Day);
                if ((dt.Month == 1) && (dt.Day == 1))
                    return 1;
                else if ((ld.Month == 12) && (ld.Day == 30))
                    return 2;
                else if ((ld.Month == 12) && (ld.Day == 29))
                {
                    Lunar.LunarDate lunarDate = new Lunar.LunarDate();
                    DateTime dateTime = DateTime.Now.AddDays(1);
                    lunarDate = Lunar.GetLunarDate(dateTime.Year, dateTime.Month, dateTime.Day);
                    if (lunarDate.Month == 1)
                        return 2;
                }
                else if ((ld.Month == 1) && (ld.Day == 1))
                    return 3;
                else if ((ld.Month == 1) && (ld.Day == 15))
                    return 4;
                else if ((ld.Month == 5) && (ld.Day == 5))
                    return 6;
                else if ((dt.Month == 5) && (dt.Day == 1))
                    return 7;
                else if ((ld.Month == 8) && (ld.Day == 15))
                    return 8;
                else if ((dt.Month == 10) && (dt.Day == 1))
                    return 9;
                else if ((dt.Month == 12) && (dt.Day == 25))
                    return 10;
                else
                {
                    SolarTerm.SolarDate[] sd = new SolarTerm.SolarDate[5];
                    sd[0] = SolarTerm.GetSolarTermDate(SolarTerm.SolarTerms.Beginning_Of_Spring, dt.Year);
                    sd[1] = SolarTerm.GetSolarTermDate(SolarTerm.SolarTerms.Beginning_Of_Summer, dt.Year);
                    sd[2] = SolarTerm.GetSolarTermDate(SolarTerm.SolarTerms.Beginning_Of_Autumn, dt.Year);
                    sd[3] = SolarTerm.GetSolarTermDate(SolarTerm.SolarTerms.Beginning_Of_Winter, dt.Year);
                    sd[4] = SolarTerm.GetSolarTermDate(SolarTerm.SolarTerms.QingMing, dt.Year);
                    if ((sd[0].Month == dt.Month) && (sd[0].Day == dt.Day))
                        return 11;
                    else if ((sd[1].Month == dt.Month) && (sd[1].Day == dt.Day))
                        return 12;
                    else if ((sd[2].Month == dt.Month) && (sd[2].Day == dt.Day))
                        return 13;
                    else if ((sd[3].Month == dt.Month) && (sd[3].Day == dt.Day))
                        return 14;
                    else if ((sd[4].Month == dt.Month) && (sd[4].Day == dt.Day))
                        return 5;
                    else
                        return 0;
                }
                return 0;
            }
            /// <summary>
            /// 天气API获取生成
            /// </summary>
            /// <returns></returns>
            public static int wgenerate()
            {
                if (File.Exists("C:\\Windows\\Temp\\wapi.html"))
                    File.Delete("C:\\Windows\\Temp\\wapi.html");
                if (File.Exists("C:\\Windows\\Temp\\lapi.html"))
                    File.Delete("C:\\Windows\\Temp\\lapi.html");
                Syscmd.ExecutePwsh("wget ip.tool.lu -o C:\\Windows\\Temp\\lapi.html", 0);
                string[] lapi = File.ReadAllLines("C:\\Windows\\Temp\\lapi.html");
                string[] json = File.ReadAllLines("C:\\IDS\\Dev\\citycode.json");
                char[] delimiterChar = { '\"', ':' };
                string ret = "";
                bool isend = false;
                for (int i = 0; i < 1952; i++)
                {
                    if (json[i].Contains("\"Province\""))
                    {
                        string[] words = json[i].Split(delimiterChar);
                        for (int j = 0; j < words.Length; j++)
                        {
                            if ((lapi[1].Contains(words[j])) && (words[j] != "") && (words[j] != " ") && (words[j] != ","))
                            {
                                for (int k = i + 1; k < 1952; k++)
                                {
                                    if (json[k].Contains("\"city\""))
                                    {
                                        string[] ct = json[k].Split(delimiterChar);
                                        for (int l = 0; l < ct.Length; l++)
                                        {
                                            if ((lapi[1].Contains(ct[l])) && (ct[l] != "") && (ct[l] != " ") && (ct[l] != ","))
                                            {
                                                isend = true;
                                                string[] words2 = json[k + 1].Split(delimiterChar);
                                                for (int m = 0; m < words2.Length; m++)
                                                {
                                                    if (words2[m].Contains("10"))
                                                        ret = words2[m];
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    if (isend)
                                        break;
                                }
                            }
                            if (isend)
                                break;
                        }
                    }
                    if (isend)
                        break;
                }
                Console.WriteLine(ret);
                string weauri = "http://www.weather.com.cn/weather1d/" + ret + ".shtml";
                Syscmd.ExecutePwsh("wget " + weauri + " -o C:\\Windows\\Temp\\wapi.html", 1000);
                string[] awp = File.ReadAllLines("C:\\Windows\\Temp\\wapi.html");
                string wapi = awp[717];
                Console.WriteLine(wapi);
                string[] vs = wapi.Split('\"');
                int sn, cl, rn, th;
                sn = cl = rn = th = 2147483647;
                for (int i = 0; i < vs.Length; i++)
                {
                    if (vs[i].Contains("晴") || vs[i].Contains("少云"))
                    {
                        sn = i;
                        break;
                    }
                }
                for (int i = 0; i < vs.Length; i++)
                {
                    if (vs[i].Contains("阴") || vs[i].Contains("多云"))
                    {
                        cl = i;
                        break;
                    }
                }
                for (int i = 0; i < vs.Length; i++)
                {
                    if (vs[i].Contains("雷"))
                    {
                        th = i;
                        break;
                    }
                }
                for (int i = 0; i < vs.Length; i++)
                {
                    if (vs[i].Contains("雨"))
                    {
                        rn = i;
                        break;
                    }
                }
                int w = 0;
                int[] arr = new int[] { sn, cl, th, rn };
                if (arr.Min() == sn)
                    w = 1;
                if (arr.Min() == cl)
                    w = 2;
                if (arr.Min() == rn)
                    w = 3;
                if (arr.Min() == th)
                    w = 4;
                Console.WriteLine(w);
                return w;
            }
            /// <summary>
            /// 时间事件生成
            /// </summary>
            /// <returns></returns>
            public static int tgenerate()
            {
                DateTime dateTime = DateTime.Now;
                if ((dateTime.Hour >= 5) && (dateTime.Hour < 11))
                    if ((dateTime.Hour >= 5) && (dateTime.Hour < 8))
                        return 1;
                    else
                        return 2;
                else if ((dateTime.Hour >= 11) && (dateTime.Hour < 14))
                    return 3;
                else if ((dateTime.Hour >= 14) && (dateTime.Hour < 20))
                    return 4;
                else if ((dateTime.Hour >= 20) && (dateTime.Hour < 24))
                    return 5;
                else if ((dateTime.Hour < 5))
                    return 6;
                else
                    return 0;
            }
        }
    }
}
