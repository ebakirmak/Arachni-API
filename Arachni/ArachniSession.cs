using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Arachni
{
    public class ArachniSession:IDisposable
    {
        private string Username { get; set; }

        private string Password { get; set; }

        private IPAddress IPAddress { get; set; }

        private int ServerPort { get; set;}

        private HttpClient Client;

        /*
         * Yetkilendirme olmadan giriş yapılır.
         * UnAuthenticate 
         */
        public ArachniSession(string ip, int port)
        {
            this.IPAddress = IPAddress.Parse(ip);
            this.ServerPort = port;
            this.Client = new HttpClient();
        }
        
        /*
         * Yetkilendirme yaparak giriş yapılır.
         * 
         */
        public ArachniSession(string username, string password, string ip, int port)
        {
            this.Username = username;
            this.Password = password;
            this.IPAddress = IPAddress.Parse(ip);
            this.ServerPort = port;

            
            
            }

        /*
         * Servisin çalışıp çalışmadığını gösterir.
         * 
         */
        public bool ArachniServiceState(string ip, int port)
        {
            HttpResponseMessage response = Client.GetAsync("http://" + ip + ":" + port+"/scans").Result;
            //HttpContent content = response.Content;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
                return false;
        }


        /*
         * Authenticate durumunu gösterir.
         * 
         */ 
        public bool Authenticate()
        {
            this.Client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("ebakirmak:1234");
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            if (true)
            {
                return true;
            }
            else
                return false;
        }

        /*
         * Arachni servisinde istenilen komutu çalıştırır.
         * 
         */ 
        public string ExecuteCommand(string command)
        {
            if (Authenticate())
            {
                string response = GETTaskAsync(this.IPAddress, this.ServerPort.ToString(), command);
                return response;
               
            }
            else
            {
                return null;
            }
                
        }



        /*
         * HttpClient
         * 
         */
        private string GETTaskAsync(IPAddress ip, string port, string command)
        {
            try
            {
                
                    Uri url = new Uri("http://" + ip + ":" + port + command);
                    Client.BaseAddress = url;
                    var response = Client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;

                        // by calling .Result you are synchronously reading the result
                        string responseString = responseContent.ReadAsStringAsync().Result;

                        //Console.WriteLine(responseString);
                        return responseString;
                    }
                    else if(response.StatusCode== HttpStatusCode.Unauthorized)
                    {
                        return "Unauthorized";
                    }
                

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
