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
    public class ScanSummaryBL
    {
        private ScanBL Scan { get; set; }

        public ScanSummaryBL()
        {

            this.Scan = new ScanBL();
        }

        /*
         * 
         * Returns the same data as "Monitor scan progress" but without issues, errors and sitemap
         */
        public ScanSummaryDL ScanSummary(ArachniManager manager, string id)
        {
            try
            {
                id = Scan.GetID(id);

                ScanSummaryDL scanDL = JsonConvert.DeserializeObject<ScanSummaryDL>(manager.GetScanSummary(id));
                return scanDL;
            }
            catch (ArgumentNullException argumentNullException)
            {

                Console.WriteLine("Arguman Null değer içermektedir:" + argumentNullException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

    }
}
