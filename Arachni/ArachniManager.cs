using Newtonsoft.Json.Linq;
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

        public string GetScans()
        {
            return  Session.ExecuteCommand("/scans");
            
        }

        public void Dispose()
        {
            this.Session.Dispose();
        }
    }
}
