using Arachni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arachni_API.BL
{
    public class Scan
    {
       public string ID { get; set; }

       public string GETScanID(ArachniManager manager)
        {
            string sonuc = manager.GetScans();
            return sonuc;
        }
    }
}
