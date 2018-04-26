using Arachni;
using Arachni_API.BL;
using Arachni_REST_API.BL;
using Arachni_REST_API.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arachni_REST_API.PL
{
    /*
     * Bu sınıf Taramaların sunum işlemlerinin(ekranla etkileşim) gerçekleştirildiği sınıftır.
     * 
     */ 
    public class ScanPL
    {
        private ScanBL Scan { get; set; }
 

        public ScanPL()
        {
            this.Scan = new ScanBL();
        }

        /*
         * Yeni bir Scan Yaratma
         * 
         */
        public  void CreateScan(ArachniManager manager)
        {

            ScanSummaryPL scanSummary = new ScanSummaryPL();
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
                    string checks = "";
                    do
                    {
                        checks = ListAndSelectCheck();
                    } while (checks == "");

                    ScanCreateDL scanCreate = new ScanCreateDL(url, checks);
                    string newScanID = Scan.CreateScan(manager, scanCreate);
                    Console.WriteLine(newScanID);
                    //Rapor
                    scanSummary.GetScanSummary(manager, newScanID);
                    break;
                }
                else
                    Console.WriteLine("URL hatalı. Kontrol edin ve Tekrar giriniz.");
            } while (true);
        }

        /*
         * Scan Duraklatma
         * 
         */
         public void PauseScan(ArachniManager manager, string id)
        {
          
            if (manager.PUTPauseScan(id)=="true")
                Console.WriteLine("Tarama durduruldu.");
            else
                Console.WriteLine("Tarama durdurulamadı.");
        }

        /*
         * Scan Başlatma
         * 
         */
         public void ResumeScan(ArachniManager manager, string id)
        {
            if (manager.PUTPauseScan(id) == "true")
                Console.WriteLine("Tarama Başlatıldı.");
            else
                Console.WriteLine("Tarama Başlatılamadı.");
        }

        /*
         * Scan Silme
         * 
         */
        public void AbortScan(ArachniManager manager, string id)
        {
            if (manager.DELETEAbortScan(id) == "true")
                Console.WriteLine("Tarama Silindi.");
            else
                Console.WriteLine("Tarama Silinemedi.");
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
         * Checks (Policy) Listeleme ve Seçne
         * 
         */
        private  string ListAndSelectCheck()
        {
            // İlgili Check'leri geri dönen değişken
            string returnChecks = "";
            // Sistemde bulunan Check Listesini sakladığımız değilken.
            ChecksBL checksBL = new ChecksBL();
            List<String> ListChecks = checksBL.ListChecks();
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
                Console.WriteLine("1 - " + ListChecks.Count + " Arası Sayı Giriniz veya tekli sayı giriniz. Örnek: 1-10,15,50-61.\n Tüm taramaları gerçekleştirmek için '*' giriniz.");
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
            catch (ArgumentOutOfRangeException argumentOutOfRangeException)
            {
                Console.WriteLine("Geçersiz bir aralık girdiniz. Aralığı kontrol edip tekrar giriş yapınız.");
                Console.WriteLine(argumentOutOfRangeException.Message);
                Thread.Sleep(4000);
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


      

    }
}
