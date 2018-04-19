using Arachni;
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

        public List<string> GETScanID(ArachniManager manager)
        {        
            JObject jObject = JObject.Parse(manager.GetScans());           
            foreach (JProperty property in jObject.Properties())
            {
                Console.WriteLine(property.Name + " - " + property.Value);
                setListScan(property.Name);
            }
            return getListScan();
        }
    }
}
