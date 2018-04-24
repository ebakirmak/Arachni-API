using Arachni;
using Arachni_API.BL;
using Arachni_REST_API.DL;
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
         * Yeni bir Scan Yaratma
         * 
         */
         private static void CreateScan(ArachniManager manager)
        {

            if (!Scan.ServiceControl(manager))
            {
                Console.WriteLine("Servis Çalışmıyor.");
                return;
            }

            do
            {
                Console.Write("URL Giriniz: ");
                string url = Console.ReadLine();
                if (ControlURL(url))
                {
                    string checks="";
                    do
                    {
                        checks = ListAndSelectCheck();
                    } while (checks=="");

                    ScanCreateDL scanCreate = new ScanCreateDL(url,checks);                   
                    Console.WriteLine(Scan.CreateScan(manager, scanCreate));
                    do
                    {
                        //Summary
                    } while (true);
                    
                }
                else
                    Console.WriteLine("URL hatalı. Kontrol edin ve Tekrar giriniz.");
            } while (true);
        }

        /*
         * Checks (Policy) Listeleme ve Seçne
         * 
         */
         private static string ListAndSelectCheck()
        {
            // İlgili Check'leri geri dönen değişken
            string returnChecks = "";
            // Sistemde bulunan Check Listesini sakladığımız değilken.
            List<String> ListChecks = Scan.ListChecks();
            //Checkleri ekrana yazdırıyoruz.
            byte counter = 1;
            foreach (var item in ListChecks)
            {
                Console.WriteLine(counter + "- " + item);
                counter += 1;
            }
            try
            {
                //Kullanıcıdan kontrol etmesini istediği check listesini alıyoruz.
                Console.WriteLine("1 - " + ListChecks.Count + " Arası Sayı Giriniz veya tekli sayı giriniz. Örnek: 1-10,15,50-61. Tüm taramaları gerçekleştirmek için '*' giriniz.");
                string checks = Console.ReadLine();

                string[] checksArray = checks.Split(',');
                foreach (var checksItem in checksArray)
                {
                    string[] checksNumber = checksItem.Split('-');
                    if (checksNumber.Count() == 1)
                    {
                        if (returnChecks == "" && checksNumber[0] != "*")
                            returnChecks += ListChecks[Convert.ToInt32(checksNumber[0]) - 1];
                        else if (checksNumber[0] == "*")
                        {
                            returnChecks = "*";
                            return returnChecks;
                        }

                        else
                            returnChecks += "," + ListChecks[Convert.ToInt32(checksNumber[0]) - 1];
                    }
                    else if (checksNumber.Count() == 2)
                    {
                        for (int i = Convert.ToInt32(checksNumber[0]) - 1; i <= Convert.ToInt32(checksNumber[1]) - 1; i++)
                        {
                            if (returnChecks == "")
                                returnChecks += ListChecks[Convert.ToInt32(i)];
                            else
                                returnChecks += "," + ListChecks[Convert.ToInt32(i)];
                        }
                    }
                }
                return returnChecks;
            }
            catch (FormatException formatException)
            {
                Console.WriteLine("İlgili Alana yalnız sayı ve - ile , girebilirsiniz. Tekrar deneyiniz.");
                Console.WriteLine(formatException.Message);
                Thread.Sleep(4000);
                return "";
            }
            catch(ArgumentOutOfRangeException argumentOutOfRangeException)
            {
                Console.WriteLine("Geçersiz bir aralık girdiniz. Aralığı kontrol edip tekrar giriş yapınız.");
                Console.WriteLine(argumentOutOfRangeException.Message);
                Thread.Sleep(4000);
                return "";
            }
            catch(Exception ex)
            {
                throw ex;
            }
         
          
           
          
            
        }

        /*
         * URL Kontrol Etme
         * 
         */
         private static bool ControlURL(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && uriResult.Scheme == Uri.UriSchemeHttp;
            if (result)
                return true;
            else
                return false;
        }
      

        /*
         * Tarama işlemlerinde yapılacak işlemler.
         * 
         */
        private static void GetScanID(ArachniManager manager)
        {
           
            List<string> listScanIDs = Scan.ScanID(manager);
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
                catch  (Exception ex)
                {
                    throw ex;
                }

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
            Console.WriteLine(  "\nStatus: " + scanResponse.Status
                               +"\nBusy: " + scanResponse.Busy);

        }

        /*
         * İlgili Taramanın Raporunu getirir.
         * 
         */
         private static void GetScanReport(ArachniManager manager,string scanID)
        {

            ScanReportDL scanReportDL = Scan.ScanReport(manager, scanID);
            Console.WriteLine("Start Date Time: " + scanReportDL.StartDatetime
                             +"\nFinish Date Time: " + scanReportDL.FinishDatetime);
            foreach (var item in scanReportDL.Issues)
            {
                Console.WriteLine("\nIssue: " + item.Name + " - Severity: " + item.Severity);
            }
            

        }

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

