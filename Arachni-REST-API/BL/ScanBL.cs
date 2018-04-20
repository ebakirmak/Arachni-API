using Arachni;
using Arachni_REST_API.DL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arachni_API.BL
{
    public class ScanBL
    {
       private string ID { get; set; }

       private List<string> ListScan { get; set; }


        public ScanBL()
        {
            this.ListScan = new List<string>();
        }

        /*
         * Bu fonksiyon Arachni cevabından gelen taramaların id'lerini Listeye ekler.
         * This function insert value id  of scans where from Arachni response.
         */
        public bool setListScan(string scanID)
        {
            try
            {
                if (scanID != null && scanID != "" && scanID.Count() != 0)
                {
                    this.ListScan.Add(scanID);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Scan::setListScan***\n" + ex.ToString());
                return false;
            }
          
              
        }

        /*
         * ListScan listesini döndürür.
         * ListScan list return.
         */
        public List<string> getListScan()
        {
            try
            {
                
                return this.ListScan;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Scan::getListScan***\n" + ex.ToString());
                return null;
            }
           
        }

        /*
         * Gelen Scan ID'leri JSON'dan ayırarak ListScan'a ekler.
         * 
         */
        public List<string> ScanID(ArachniManager manager)
        {
            try
            {
                string responseBody = manager.GetScans();
                if (responseBody == "Unauthorized")
                {
                    return null;
                }
                else
                {
                    JObject jObject = JObject.Parse(responseBody);
                    this.ListScan = new List<string>();
                    foreach (JProperty property in jObject.Properties())
                    {
                        setListScan(property.Name);
                    }

                    return getListScan();
                }
               
            }
            catch (Exception ex)
            {

                Console.WriteLine("ScanBL::GETScanID\n"+ex.Message);
                return null;
            }
            
        }

        /*
         * 
         * Monitor scan progress
         */
        public ScanMonitorDL ScanMonitor (ArachniManager manager,string id)
        {
           
            string ScanMonitorJson = manager.GetScanMonitor(id);
            ScanMonitorDL scanDL;
            if (ScanMonitorJson!=null)
                scanDL  = JsonConvert.DeserializeObject<ScanMonitorDL>(ScanMonitorJson);
            else
                return null;

            return scanDL;
        }


        /*
         * 
         * Returns the same data as "Monitor scan progress" but without issues, errors and sitemap
         */
        public ScanSummaryDL ScanSummary(ArachniManager manager,string id)
        {
            

            ScanSummaryDL scanDL = JsonConvert.DeserializeObject<ScanSummaryDL>(manager.GetScanSummary(id));
            return scanDL;
        }

        /*
         * 
         * Retrieve a scan report
         */
        public ScanReportDL ScanReport(ArachniManager manager,string id)
        {
            
            ScanReportDL scanReportDL = JsonConvert.DeserializeObject<ScanReportDL>(manager.GetScanReport(id,"json"));
            
            return scanReportDL;
        }

        /*
         * Tarama Raporunu Json'a çevirir ve kaydeder. Masaüstüne.
         * 
         */
         public bool ScanReportToJson(ArachniManager manager, ScanReportDL scan)
        {
            string json = JsonConvert.SerializeObject(scan);
            //Tarama raporunu kaydet..
            return true;
        }

        /*
         * Yeni bir Scan Oluşturur.
         * 
         */ 
         public string CreateScan(ArachniManager manager, ScanCreateDL scan)
        {
            string json = JsonConvert.SerializeObject(scan);
            string id = manager.SetScanCreate(json);
            return id;
        }

        /*
         * Servisin çalışıp çalışmadığını kontrol eder.
         * 
         */
        public bool ServiceControl(ArachniManager manager)
        {
            return manager.GetServiceControl();
        }
    }
}
