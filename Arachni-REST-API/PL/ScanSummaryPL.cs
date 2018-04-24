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
    public class ScanSummaryPL
    {
        
        private ScanReportPL ScanReportPL { get; set; }
        private ScanSummaryBL Summary { get; set; }

        public ScanSummaryPL()
        {
         
            this.ScanReportPL = new ScanReportPL();
            this.Summary = new ScanSummaryBL();
        }
        /*
         * İlgili Taramanın Özetini getirir.
         * 
         */
        public void GetScanSummary(ArachniManager manager, string scanID)
        {
            //Tuş basma olayı...
            ConsoleKeyInfo cki;
            //Summary
            ScanSummaryDL scanSummaryDL;
            Console.WriteLine("Taramanın Son durumunu görmek için herhangi bir tuşa, Çıkmak için  ESC tuşuna basınız.");
            do
            {
                //Summary
                scanSummaryDL = Summary.ScanSummary(manager, scanID);
                Console.WriteLine("\nStatus: " + scanSummaryDL.Status
                                 + "\nBusy: " + scanSummaryDL.Busy
                                 + "\nRuntime: " + scanSummaryDL.Statistics.Runtime);
                Thread.Sleep(3000);


                cki = Console.ReadKey(true);
                if (scanSummaryDL.Status == "done" && scanSummaryDL.Busy == false)
                {
                    Console.WriteLine("Tarama bitti. Tarama raporunu ekranda gösterildi ve masaüstüne indirildi.");
                    ScanReportPL.GetScanReport(manager, scanID);
                    break;
                }

            } while (cki.Key != ConsoleKey.Escape);

        }
    }
}
