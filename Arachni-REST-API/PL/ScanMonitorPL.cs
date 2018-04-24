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
    public class ScanMonitorPL
    {
        private ScanMonitorBL Monitor { get; set; }

        public ScanMonitorPL()
        {
            this.Monitor = new ScanMonitorBL();
        }

        /*
         * İlgili Taramayı monitor eder / izler.
         * 
         */
        public void GetScanMonitor(ArachniManager manager, string scanID)
        {

            ScanMonitorDL scanResponse = Monitor.ScanMonitor(manager, scanID);

            Console.WriteLine("\nStatus: " + scanResponse.Status +
                              "\nBusy: " + scanResponse.Busy);
        }
    }
}
