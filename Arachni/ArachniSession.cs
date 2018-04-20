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
         * HttpClient Authenticate eder.
         * 
         */ 
        public bool Authenticate()
        {
            try
            {
                this.Client = new HttpClient();
                var byteArray = Encoding.ASCII.GetBytes("ebakirmak:1234");
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ArachniSession::Authenticate " + ex.Message);
                return false;
            }           
         
        }
       
        /*
         * Servisin çalışıp çalışmadığını gösterir.
         * 
         */
        public bool ArachniServiceState()
        {
            try
            {
                if (Authenticate())
                {

                    Uri serviceUrl = new Uri("http://" + this.IPAddress + ":" + this.ServerPort + "/scans");
                    Client.BaseAddress = serviceUrl;
                    var response = Client.GetAsync(serviceUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return false;
                    }
                    return false;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("ArachniSession::ArachniServiceState " + ex.Message);
                return false;
            }
            
        }


        /*
         * Arachni servisinde istenilen komutu çalıştırır.
         * 
         */
        public string GetExecuteCommand(string command)
        {
            try
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
            catch (Exception ex)
            {

                Console.WriteLine("ArachniSession::GetExecuteCommand" + ex.Message);
                return null;
            }
          
                
        }

        /*
         * Arachni servisinde istenilen komutu çalıştırır.
         * 
         */ 
        public string POSTExecuteCommand(string command,string json)
        {
            try
            {
                if (Authenticate())
                {

                    string response = POSTTaskAsync(this.IPAddress, this.ServerPort.ToString(), command, json);
                    return response;
                }

                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ArachniSession::POSTExecuteCommand " + ex.Message);
                return null;
            }
          
        }



        /*
         * HttpClient GET 
         * 
         */
        private string GETTaskAsync(IPAddress serviceHost, string servicePort, string command)
        {
            try
            {
                
                    Uri serviceUrl = new Uri("http://" + serviceHost + ":" + servicePort + command);
                    Client.BaseAddress = serviceUrl;
                    var response = Client.GetAsync(serviceUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;

                        // by calling .Result you are synchronously reading the result
                        string responseString = responseContent.ReadAsStringAsync().Result;

                        //Console.WriteLine(responseString);
                        return responseString;
                    }
                   
                

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        /*
         * HttpClient POST
         * 
         */
        private string POSTTaskAsync(IPAddress serviceHost, string servicePort, string command,string json)
        {
            try
            {

                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                Uri serviceUrl = new Uri("http://" + serviceHost + ":" + servicePort + command);
                Client.BaseAddress = serviceUrl;
          
                var response = Client.PostAsync(serviceUrl,jsonContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    //Console.WriteLine(responseString);
                    return responseString;
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
