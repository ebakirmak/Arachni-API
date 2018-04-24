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
    /*
     * Bu sınıf İzleme ile ilgili işlemlerin yapıldığı sınıftır.
     * 
     */ 
    public class ScanMonitorBL
    {
        private ScanBL ScanBL { get; set; }

        public ScanMonitorBL()
        {
            this.ScanBL = new ScanBL();
        }

        /*
         * Monitor scan progress
         * 
         */
        public ScanMonitorDL ScanMonitor(ArachniManager manager, string id)
        {
            id = ScanBL.GetID(id);
            string ScanMonitorJson = manager.GetScanMonitor(id);
            ScanMonitorDL scanDL;
            if (ScanMonitorJson != null)
                scanDL = JsonConvert.DeserializeObject<ScanMonitorDL>(ScanMonitorJson);
            else
                return null;

            return scanDL;
        }
    }
}
