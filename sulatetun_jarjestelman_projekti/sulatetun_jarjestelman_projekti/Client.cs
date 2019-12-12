using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Threading;

namespace sulatetun_jarjestelman_projekti
{
    class Client
    {
        const string URL = "http://ec2-35-172-180-97.compute-1.amazonaws.com";
        public int speedControl { get; set; }
        public int turnControl { get; set; }
        public int button { get; set; }
        public int repeatControl { get; set; }

        public int Deg { get; set; }
        public int Speed { get; set; }

        public int connectionCheck { get; set; }

        public int gyroID = 0;

        private string resultJson;

        const int BUFFER_SIZE = 1024;

        //deserialize json https://www.youtube.com/watch?v=CjoAYslTKX0
        //post https://www.youtube.com/watch?v=_6IdfPMxYFI
        //https://github.com/lassehav-oamk/express-api-demo/blob/mysql-integration/components/dogs.js
        //https://stackoverflow.com/questions/1056121/how-to-create-json-string-in-c-sharp
        //https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to

        public string deserialize(string strJson)
        {
            try
            {
                string jpcIn = JsonConvert.DeserializeObject<dynamic>(strJson);
                return jpcIn;
            }
            catch (Exception ex)
            {
                Console.WriteLine("deserialize Error " + ex.ToString());
                return null;
            }
        }

        public string serialize(Object obj)
        {
            try
            {
                resultJson = JsonConvert.SerializeObject(obj); 
                return resultJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine("serialize Error " + ex.ToString());
                return null;
                
            }
        }

        private Dictionary<string, string> serialize2(string speed, string turn, string button)
        {
            var values = new Dictionary<string, string>
            {
                { "speedControl", speed },
                { "turnControl", turn },
                { "button", button }
            };

            return values;
        }

        public void getGyro()
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "/rb");
            /////
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                    dynamic value = JsonConvert.DeserializeObject(html);
                    try
                    {
                        Deg = value[0].Deg;
                        Speed = value[0].Speed;
                        gyroID = value[0].idrbOut;
                    }
                    catch (Exception ex)
                    {
                        Deg = 0;
                        Speed = 0;
                        Console.WriteLine("getGyro Error 2 " + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("getGyro Error 1 " + ex.ToString());
            }
            
        }

        public int checkFromerData()
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "/rb/check");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                    dynamic value = JsonConvert.DeserializeObject(html);
                    try
                    {
                        if (value[0].connectionCheck == 1)
                        {
                            return 0;
                        }
                        else
                            return 1;
                    }
                    catch (Exception ex)
                    {
                        // string tempjson = "{'speedControl': '0', 'turnControl': '0', 'button': '0'}";
                        string tempjson = this.serialize(this);
                        postRequest(tempjson);
                        Console.WriteLine("checkFromerData Error 2 " + ex.ToString());
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("checkFromerData Error 1 " + ex.ToString());
                return 2;
            }
            //return value[0].connectionCheck;
        }

        public bool playStopped()
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "/playstop");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                    dynamic value = JsonConvert.DeserializeObject(html);
                    try
                    {
                        if (value[0] == 1)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        // string tempjson = "{'speedControl': '0', 'turnControl': '0', 'button': '0'}";
                        Console.WriteLine("playStopped Error 2" + ex.ToString());
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("playStopped Error 1 " + ex.ToString());
                return false;
            }
            //return value[0].connectionCheck;
        }

        //https://stackoverflow.com/questions/41960201/c-sharp-httpwebrequest-post-with-loop
        //https://stackoverflow.com/questions/13261046/executing-web-requests-inside-of-a-loop-only-works-on-the-first-pass
        public async void postRequest(string postJson)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL + "/pc");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            var bytes = Encoding.UTF8.GetBytes(resultJson);
            httpWebRequest.ContentLength = resultJson.Length;
            httpWebRequest.KeepAlive = false;
            httpWebRequest.Proxy = null;

            using (var requestStream = await httpWebRequest.GetRequestStreamAsync())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                httpWebRequest.Abort();
                requestStream.Flush();
                requestStream.Close();
                requestStream.Dispose();
                httpWebRequest = null;
            }
            
        }
    }
}
