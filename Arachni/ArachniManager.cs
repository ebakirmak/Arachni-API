using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arachni
{
    public class ArachniManager:IDisposable
    {
        private ArachniSession Session { get; set; }

        public ArachniManager(ArachniSession session)
        {
            if (session != null)
            {
                this.Session = session;
            }
        }


        /*--------------------------------------------------------------------------------------------------------*/
        
       /*
        * Bu fonksiyon yeni bir tarama oluşturulmasını sağlar.         
        * 
        */
        public string POSTScanCreate(string json)
        {

            return Session.POSTExecuteCommand("/scans", json);
        }

        /*
         * Bu fonksiyon tüm taramaların idlerini döndürür.
         * This function return all scans of ids.
         */
        public string GetScans()
        {
            return  Session.GetExecuteCommand("/scans");
            
        }

        /*
         * Bu fonksiyon ilgili taramayı durdurur.
         * 
         */
         public string PUTPauseScan(string id)
        {
            return Session.PUTExecuteCommand("/scans/" + id + "/pause");
        }

        /*
        * Bu fonksiyon ilgili taramayı devam ettirir.
        * 
        */
        public string PUTResumeScan(string id)
        {
            return Session.PUTExecuteCommand("/scans/" + id + "/resume");
        }

        /*
        * Bu fonksiyon ilgili taramayı siler.
        * 
        */
        public string DELETEAbortScan(string id)
        {
            return Session.DeleteExecuteCommand("/scans/" + id);
        }

        /*---------------------------------------------------------------------------------------------------------------*/

        /*
         * Bu fonksiyon ilgili taramanın bilgilerini getirir.
         * 
         */
        public string GetScanMonitor(string id)
        {
            return Session.GetExecuteCommand("/scans/" + id);
        }


        /*
         * Bu fonksiyon ilgili taramanın
         *      eğer bitmiş ise özetini
         *      devam ediyorsa süreç bilgisini döndürür.
         * This function return 
         *      if scans ended of scan summary
         *      if scans didn't  end of scan progress
         */
         public string GetScanSummary(string id)
        {
            return Session.GetExecuteCommand("/scans/" + id+ "/summary");
        }

        /*
         * Bu Fonksiyon ilgili taramayı istenilen formatta almayı sağlar.
         * 
         */
         public string GetScanReport(string id,string type)
        {
            return Session.GetExecuteCommand("/scans/" + id + "/report." + type);
        }

       

        /*
         * Servisin çalışıp çalışmadığını kontrol eder.
         * 
         */
         public bool GetServiceControl()
        {
            return Session.ArachniServiceState();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
