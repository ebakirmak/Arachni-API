using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
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
         * HttpClient SSL Sertifikası ile bağlanır (verilerin şifreli iletimi için) ve
         * ilgili sunucuda username ve parola ile basit yetkilendirme (Basic Authhentication) işlemi yapılır. (Çözümü araştırılıyor.)
         * 
         * 
         */      

        //public bool AuthenticateSSL()
        //{
        //    try
        //    {  
        //        // The path to the certificate.
        //        string Certificate = @"C:\Users\emreakirmak\Desktop\key\arachnicertificate.p12";

        //        WebRequestHandler handler = new WebRequestHandler();
        //        handler.ClientCertificates.Add(new X509Certificate(Certificate, "*0209*1903"));
        //        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
             


        //        this.Client = new HttpClient(handler);
                
        //        var byteArray = Encoding.ASCII.GetBytes("ebakirmak:1234");
        //        Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("ArachniSession::Authenticate " + ex.Message);
        //        return false;
        //    }

        //}
     




        /*
         * HttpClient ilgili sunucuda username ve parola ile basit yetkilendirme (Basic Authentication) işlemi yapılır.
         * 
         */
        public bool Authenticate()
        {
            try
            {
                this.Client = new HttpClient();
                //var byteArray = Encoding.ASCII.GetBytes("ebakirmak:1234");
                var byteArray = Encoding.ASCII.GetBytes(this.Username+":"+this.Password);
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
                        Console.WriteLine("Hatalı username veya parola");
                        return false;
                    }
                    return false;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine("\nREST-Server çalışmıyor. \nHost adresini, Port numarasını ve Servisin çalışıp çalışmadığını kontrol ediniz.\n" + ex.Message);
                //Console.WriteLine("ArachniSession::ArachniServiceState " + ex.Message);
                return false;
            }
            
        }

        /*---------------------------------------------------------------------------------------------------------*/

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
         * Arachni servisinde istenilen komutu çalıştırır.
         * 
         */
        public string PUTExecuteCommand(string command)
        {
            try
            {
                if (Authenticate())
                {
                    string response = PUTTaskAsync(this.IPAddress, this.ServerPort.ToString(), command);
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
        public string DeleteExecuteCommand(string command)
        {
            try
            {
                if (Authenticate())
                {
                    string response = DeleteTaskAsync(this.IPAddress, this.ServerPort.ToString(), command);
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
         * HttpClient GET isteği ile istenilen komutu çalıştırır.
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
                Console.WriteLine("Server'a ulaşılmıyor." + ex.Message);
                return null;
            }
        }


        /*-------------------------------------------------------------------------------------------------------------------*/

        /*
         * HttpClient POST isteği ile istenilen komutu çalıştırır.
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

        /*
        * HttpClient PUT isteği ile istenilen komutu çalıştırır.
        * 
        */
        private string PUTTaskAsync(IPAddress serviceHost, string servicePort, string command)
        {
            try
            {

                Uri serviceUrl = new Uri("http://" + serviceHost + ":" + servicePort + command);
                Client.BaseAddress = serviceUrl;
                var response = Client.PutAsync(serviceUrl,null).Result;

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
                Console.WriteLine("Server'a ulaşılmıyor." + ex.Message);
                return null;
            }
        }

       /*
        * HttpClient Delete isteği ile istenilen komutu çalıştırır.
        * 
        */
        private string DeleteTaskAsync(IPAddress serviceHost, string servicePort, string command)
        {
            try
            {

                Uri serviceUrl = new Uri("http://" + serviceHost + ":" + servicePort + command);
                Client.BaseAddress = serviceUrl;
                var response = Client.DeleteAsync(serviceUrl).Result;

                if (response.IsSuccessStatusCode)
                {

                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    //Console.WriteLine(responseString);
                    return "true";
                }
                



                return null;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Server'a ulaşılmıyor." + ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
