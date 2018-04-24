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
  
    class Program
    {
        //static string id = "293bbf3bf3f81fb1de188f49e78720a0";
        static ScanBL Scan = new ScanBL();
        static void Main(string[] args)
        {
            using (ArachniSession session = new ArachniSession("ebakirmak","1234","206.189.12.255", 443))
            {
                using (ArachniManager manager = new ArachniManager(session))
                {
                    do
                    {
                        Console.Write("A - Raporları Göster\n" +
                            "B - Yeni Scan Oluştur\n" +
                            "C - Servisi Kontrol Et\n" +
                            "Lütfen yapmak istediğiniz işlemi seçiniz: ");
                        string selectedProcess = Console.ReadLine();

                        if (selectedProcess == "A")
                        {
                            GetScanID(manager);
                        }
                        else if (selectedProcess == "B")
                        {
                            CreateScan(manager);
                        }
                        else if (selectedProcess == "C")
                        {
                            ServiceControl(manager);
                        }
                        Console.WriteLine("\n");
                    } while (true);
                }
            }            
        }

    
        /*
         * Tarama Oluşturma
         * 
         */ 
        private static void CreateScan(ArachniManager manager)
        {
            ScanPL scanPL = new ScanPL();
            scanPL.CreateScan(manager);
        }  

        /*-------------------------------------------------------------------------------------------------------------*/



        /*
         * İlgili taramada yapılacak işlemler.
         * 
         */
        private static void GetScanID(ArachniManager manager)
        {
           
            List<string> listScanIDs = Scan.ScanID(manager);
            if (listScanIDs != null && listScanIDs.Count > 0)
            {
                int i = 0;
                foreach (var item in listScanIDs)
                {
                    Console.WriteLine((i+1) + " - " + item);
                    i += 1;
                }
                string id = SelectScan(listScanIDs);
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
            else if(listScanIDs.Count == 0)
            {
                Console.WriteLine("Tarama bulunamadı...");
                return;
            }
            else
            {
                Console.WriteLine("Unauthorized");
                return;
            }


        }


        /*
         * Tarama Seçme İşlemi
         * 
         */
        private static string SelectScan(List<string> listScanIDs)
        {
            Console.Write("Hangi Taramayı Görüntülemek İstiyorsunuz? : ");

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
                    Console.Write("Seçiminizi kontrol ediniz. Hangi Taramayı Görüntülemek İstiyorsunuz? : ");
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
    }

}

