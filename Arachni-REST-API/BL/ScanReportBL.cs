using Arachni;
using Arachni_API.BL;
using Arachni_REST_API.DL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arachni_REST_API.BL
{
    public class ScanReportBL
    {
        public ScanBL Scan { get; set; }

        public ScanReportBL()
        {
            this.Scan = new ScanBL();
        }
        /*
        * Tarama raporunu kayıt eder (xml formatında masaüstüne) ve ScanReportDL nesnesini döndürür.
        * Retrieve a scan report
        */
        public ScanReportDL ScanReport(ArachniManager manager, string id)
        {
            id = Scan.GetID(id);
            string report = manager.GetScanReport(id, "xml");
            SaveReport(report);


            report = manager.GetScanReport(id, "json");
            ScanReportDL scanReportDL = JsonConvert.DeserializeObject<ScanReportDL>(report);
            //SaveReport(report);
           
            return scanReportDL;
        }

        /*
         * İlgili string dizisini kaydet.
         * 
         */
        public bool SaveReport(string report)
        {
            try
            {
                string strPath = Environment.GetFolderPath(
                           System.Environment.SpecialFolder.DesktopDirectory);
                System.IO.File.WriteAllText(strPath + "\\Aranchi.xml", report.ToString());
                Console.WriteLine("Rapor Masaüstüne Arachni.xml olarak kayıt edildi.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

    }
}
