using Arachni;
using Arachni_API.BL;
using Arachni_REST_API.DL;
using Arachni_REST_API.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arachni_REST_API
{
    /*
     * REST-Server kurulumu için -> https://github.com/Arachni/arachni/wiki/REST-server
     * REST-API istekleri için -> https://github.com/Arachni/arachni/wiki/REST-API
     * Program MVC mantığında 3 katman halinde kodlanmıştır.
     *      1. Katman - PL Katmanı: View katmanıdır.        Kullanıcı ile etkileşimlerin, çıktıların olduğu katman.
     *      2. Katman - DL Katmanı: Model katmanıdır.       Temsili sınıfların tutulduğu katmandır. JSON isteklerinde kullanıldı. Veritabanı işlemleri için kullanılabilir.
     *      3. Katman - BL Katmanı: Controller Katmanıdır.  JSON isteklerinin çağrıldığı view ile iletişime geçilen katmandır. 
     */

    class Program
    {
        //static string id = "293bbf3bf3f81fb1de188f49e78720a0";
        static ScanBL Scan = new ScanBL();

        //Server IP
        private static string IP { get; set; }
        //Server Port
        private static int Port { get; set; }
        //Username
        private static string Username { get; set; }
        //Password
        private static string Password { get; set; }

        static void Main(string[] args)
        {
            
            try
            {
                SetIPAndPort();

                using (ArachniSession session = new ArachniSession(Username,Password,IP,Port ))
                {
                    using (ArachniManager manager = new ArachniManager(session))
                    {
                        if (!manager.GetServiceControl())
                        {
                         
                            Console.Read();
                            return;
                        }
                            

                        while (manager.GetServiceControl())
                        {
                            Console.Write("A - Raporları Göster\n" +
                                "B - Tarama Oluştur\n" +
                                "C - Servisi Kontrol Et\n" +
                                "P - Taramayı Durdur\n" +
                                "R - Taramayı Tekrar Başlat\n" +
                                "D - Taramayı Sil\n"+
                                "Lütfen yapmak istediğiniz işlemi seçiniz: ");
                            string selectedProcess = Console.ReadLine();

                            if (selectedProcess.ToUpper() == "A")
                            {
                                GetScanID(manager);
                            }
                            else if (selectedProcess.ToUpper() == "B")
                            {
                                CreateScan(manager);
                            }
                            else if (selectedProcess.ToUpper() == "C")
                            {
                                ServiceControl(manager);
                            }
                            else if (selectedProcess.ToUpper() == "P")
                            {
                                PauseScan(manager);
                            }
                            else if(selectedProcess.ToUpper() == "R")
                            {
                                ResumeScan(manager);
                            }
                            else if (selectedProcess.ToUpper() == "D")
                            {
                                AbortScan(manager);
                            }
                            Console.WriteLine("\n");
                        }

                    }
                }

             
            }
            catch (Exception ex)
            {

                throw ex;
            }
                  
        }

        /*
         * IP adresi ve port numarası girme
         * 
         */
         private static void SetIPAndPort( )
        {
            do
            {
                try
            {
            
                    Console.Write("IP Adresi ve Port Adresini değiştirmek istiyor musunuz?(E/H)");
                    string selected = Console.ReadLine().ToUpper();
                    if (selected == "E")
                    {
                        Console.Write("IP Adresini Giriniz: ");
                        IP = Console.ReadLine();

                        Console.Write("Port Numarasını Giriniz: ");
                        Port = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Username Giriniz: ");
                        Username = Console.ReadLine();

                        Console.Write("Parola Giriniz: ");
                        Password = Console.ReadLine();
                        break;
                    }
                    else if (selected == "H")
                    {
                        IP = "206.189.12.255";
                        Port = 443;
                        Username = "ebakirmak";
                        Password = "1234";
                        break;
                    }
            
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input format biçimi hatalı. Kontrol ediniz." + e.Message);
                //throw;
            }
        } while (true);
         
           
        }


        /*------------------------------------------------------------------------------------------------------------------------------*/
    
        /*
         * Tarama Oluşturma
         * 
         */
        private static void CreateScan(ArachniManager manager)
        {
            ScanPL scanPL = new ScanPL();
            scanPL.CreateScan(manager);
        }  

        /*
         * Tarama Durdurma
         * 
         */ 
        private static void PauseScan(ArachniManager manager)
        {
            ScanPL scanPL = new ScanPL();
            string id = ListScan(manager);
            scanPL.PauseScan(manager,id);
        }

        /*
         * Tarama devam ettirme
         * 
         */ 
        private static void ResumeScan(ArachniManager manager)
        {
            ScanPL scanPL = new ScanPL();
            string id = ListScan(manager);
            scanPL.ResumeScan(manager, id);
        }

        /*
         * Tarama Silme
         * 
         */ 
        private static void AbortScan(ArachniManager manager)
        {
            ScanPL scanPL = new ScanPL();
            string id = ListScan(manager);
            scanPL.AbortScan(manager, id);
        }
        /*-------------------------------------------------------------------------------------------------------------*/



        /*
         * Taramada yapılabilecek işlemler.
         * 
         */
        private static void GetScanID(ArachniManager manager)
        {

            string id = ListScan(manager);
            if (id==null)
            {
                return;
            }
            Console.Write("A - Taramayı İzle (Monitor)\n" +
                          "B - Tarama Özeti\n" +
                          "C - Tarama Raporu\n");
            Console.Write("İstediğiniz Rapor Tipini Giriniz: ");
            string selectedProcess = Console.ReadLine();

            switch (selectedProcess)
            {
                case "A":
                    GetScanMonitor(manager, id);
                    break;
                case "B":
                    GetScanSummary(manager, id);
                    break;
                case "C":
                    GetScanReport(manager, id);
                    break;
                default:
                    break;

            }

        }      
       

        /*
         * Tarama Raporu Getir
         * 
         */
        private static void GetScanReport(ArachniManager manager, string id)
        {
            ScanReportPL scanReportPL = new ScanReportPL();
            scanReportPL.GetScanReport(manager, id);
        }

        /*
         * Tarama Özeti Getir
         * 
         */
        private static void GetScanSummary(ArachniManager manager, string id)
        {
            ScanSummaryPL scanSummaryPL = new ScanSummaryPL();
            scanSummaryPL.GetScanSummary(manager, id);
        }

        /*
         * Tarama İzle
         * 
         */
        private static void GetScanMonitor(ArachniManager manager, string id)
        {
            ScanMonitorPL scanMonitorPL = new ScanMonitorPL();
            scanMonitorPL.GetScanMonitor(manager, id);
        }


        
      /*------------------------------------------------------------------------------------------------*/  
        
        
        /*
         * Servisin çalışıp çalışmadığını kontrol eder.
         * 
         */
        private static void ServiceControl(ArachniManager manager)
        {
            if (Scan.ServiceControl(manager))
                Console.WriteLine("Servis çalışıyor.");
            else
                Console.WriteLine("Servis çalışmıyor.");
        }

        /*
         * Tüm taramaları listeleme
         * 
         */
         private static string ListScan(ArachniManager manager)
        {
            List<string> listScanIDs = Scan.ScanID(manager);
            if (listScanIDs != null && listScanIDs.Count > 0)
            {
                int i = 0;
                foreach (var item in listScanIDs)
                {
                    Console.WriteLine((i + 1) + " - " + item);
                    i += 1;
                }
                string id = SelectScan(listScanIDs);
                return id;
            }
            else if (listScanIDs == null)
            {
                return null;
            }
            else if (listScanIDs.Count == 0)
            {
                Console.WriteLine("Tarama bulunamadı...");
                return null;
            }
            else
            {
                Console.WriteLine("Unauthorized");
                return null;
            }

        }

        /*
        * Tarama Seçme İşlemi
        * 
        */
        private static string SelectScan(List<string> listScanIDs)
        {
            Console.Write("Hangi Taramayı Seçmek İstiyorsunuz? : ");

            bool state = false;
            do
            {
                try
                {
                    int taskID = Convert.ToInt32(Console.ReadLine());
                    if (listScanIDs.Count >= taskID)
                    {
                        return listScanIDs[taskID - 1];
                    }
                    Console.Write("Seçiminizi kontrol ediniz. Hangi Taramayı Seçmek İstiyorsunuz? : ");
                }
                catch (FormatException)
                {
                    Console.Write("Hatalı Giriş. Tekrar Deneyiniz.");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            } while (state == false);


            return "0";
        }
    }

}

