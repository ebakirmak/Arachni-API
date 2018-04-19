using Newtonsoft.Json.Linq;
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

        public ArachniSession(string host, int port)
        {
            this.IPAddress = IPAddress.Parse(host);
            this.ServerPort = port;
        }

        public ArachniSession(string username, string password, string host, int port)
        {
            this.Username = username;
            this.Password = password;
            this.IPAddress = IPAddress.Parse(host);
            this.ServerPort = port;
        }

        /*
         * Servisin çalışıp çalışmadığını gösterir.
         * 
         */
        public bool ArachniServiceState()
        {
            if (true)
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
                using (var client = new HttpClient())
                {
                    string url = "http://" + ip + ":" + port + command;
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;

                        // by calling .Result you are synchronously reading the result
                        string responseString = responseContent.ReadAsStringAsync().Result;

                        Console.WriteLine(responseString);
                        return responseString;
                    }
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
           
        }
    }
}
