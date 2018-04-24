using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
         * SSL Sertifika
         * 
         */
        // public ArachniSession(string username,string password, string ip, int port)
        //{
        //    //Do webrequest to get info on secure site
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://mail.google.com");
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    response.Close();

        //    //retrieve the ssl cert and assign it to an X509Certificate object
        //    X509Certificate cert = request.ServicePoint.Certificate;

        //    //convert the X509Certificate to an X509Certificate2 object by passing it into the constructor
        //    X509Certificate2 cert2 = new X509Certificate2(cert);

        //    string cn = cert2.IssuerName.ToString();
        //    string cedate = cert2.GetExpirationDateString();
        //    string cpub = cert2.GetPublicKeyString();

        //    //display the cert dialog box
            
        //}
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
