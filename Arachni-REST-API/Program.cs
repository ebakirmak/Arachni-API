using Arachni;
using Arachni_API.BL;
using Arachni_REST_API.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                            "Lütfen yapmak istediğiniz işlemi seçiniz: ");
                        string selectedProcess = Console.ReadLine();

                        if (selectedProcess == "A")
                        {
                            GetScanID(manager);
                        }
                        Console.WriteLine("\n");
                    } while (true);
                }
            }            
        }

        /*
         * Tarama işlemlerinde yapılacak işlemler.
         * 
         */
        private static void GetScanID(ArachniManager manager)
        {
           
            List<string> listScanIDs = Scan.GETScanID(manager);
            if (listScanIDs != null)
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
                string selectedProcess = Console.ReadLine();

                switch (selectedProcess)
                {
                    case "A":
                        GetScanMonitor(manager, id);
                        break;
                    case "B":
                        GetScanSummary(manager, id);
                        break;
                    default:
                        break;

                }

            }
            else
            {
                Console.WriteLine("Unauthorized");
                return;
            }


        }


        private static string SelectScan(List<string> listScanIDs)
        {
            Console.Write("Hangi Taramayı Görüntülemek İstiyorsunuz? : ");

            bool state = false;
            do
            {
                int taskID = Convert.ToInt32(Console.ReadLine());
                if (listScanIDs.Count >= taskID)
                {
                    return listScanIDs[taskID - 1];                    
                }
                Console.Write("Seçiminizi kontrol ediniz. Hangi Taramayı Görüntülemek İstiyorsunuz? : ");
            } while (state == false);

            return "0";
        }

        /*
         * İlgili Taramayı monitor eder / izler.
         * 
         */
         private static void GetScanMonitor(ArachniManager manager, string scanID)
        {        

            ScanMonitorDL scanResponse = Scan.ScanMonitor(manager, scanID);
            Console.WriteLine("\nStatus: " + scanResponse.Status+
                              "\nBusy: "   + scanResponse.Busy);
        }

        /*
         * İlgili Taramanın Özetini getirir.
         * 
         */
         private static void GetScanSummary(ArachniManager manager,string scanID)
        {
            ScanSummaryDL scanResponse = Scan.ScanSummary(manager, scanID);

        }


    }

}

