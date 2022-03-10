# DrBlackService-Voice
开机语音播报服务  
本服务被用于DrBlack System中的Bilibili OS 4.0定制系统  
因为某些原因将其开源  

在Service1.cs中165行涉及敏感数据,已被删除
``` C#
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
```
其他区域均未进行修改
