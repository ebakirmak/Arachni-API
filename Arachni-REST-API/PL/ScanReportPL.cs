using Arachni;
using Arachni_API.BL;
using Arachni_REST_API.BL;
using Arachni_REST_API.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arachni_REST_API.PL
{
    public class ScanReportPL
    {
        private ScanReportBL Report { get; set; }

        public ScanReportPL()
        {
            this.Report = new ScanReportBL();
        }

        /*
         * İlgili Taramanın Raporunu getirir.
         * 
         */
        public  void GetScanReport(ArachniManager manager, string scanID)
        {
            

            ScanReportDL scanReportDL = Report.ScanReport(manager, scanID);
            Console.WriteLine("Start Date Time: " + scanReportDL.StartDatetime
                             + "\nFinish Date Time: " + scanReportDL.FinishDatetime);
            foreach (var item in scanReportDL.Issues)
            {
                Console.WriteLine("\nIssue: " + item.Name + " - Severity: " + item.Severity);
            }


        }
    }
}
