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
    
        /*
         * Bu fonksiyon tüm taramaların idlerini döndürür.
         * This function return all scans of ids.
         */
        public string GetScans()
        {
            return  Session.ExecuteCommand("/scans");
            
        }

        /*
         * 
         * 
         */
         public string GetScanMonitor(string id)
        {
            return Session.ExecuteCommand("/scans/" + id);
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
            return Session.ExecuteCommand("/scans/" + id+ "/summary");
        }

        /*
         * Bu Fonksiyon ilgili taramayı istenilen formatta almayı sağlar.
         * 
         */
         public string GetScanReport(string id,string type)
        {
            return Session.ExecuteCommand("/scans/" + id + "/report." + type);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
