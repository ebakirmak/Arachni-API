using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arachni_REST_API.DL
{
    
       
   
        public partial class ScanCreateDL
        {
            public ScanCreateDL(string url, string checks)
            {
                this.Url = url;
                this.Checks = new string[checks.Split(',').Count()];
                int counter = 0;
                foreach (var item in checks.Split(','))
                {
                    this.Checks[counter] = item;
                    counter++;
                }
                       
            }
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("checks")]
            public string[] Checks { get; set; }

            //[JsonProperty("audit")]
            //public Audit Audit { get; set; }

            //[JsonProperty("scope")]
            //public Scope Scope { get; set; }
        }

        public partial class Audit
        {
            [JsonProperty("links")]
            public bool Links { get; set; }

            [JsonProperty("forms")]
            public bool Forms { get; set; }

            [JsonProperty("cookies")]
            public bool Cookies { get; set; }

            [JsonProperty("headers")]
            public bool Headers { get; set; }

            [JsonProperty("jsons")]
            public bool Jsons { get; set; }

            [JsonProperty("xmls")]
            public bool Xmls { get; set; }

            [JsonProperty("ui_inputs")]
            public bool UiInputs { get; set; }

            [JsonProperty("ui_forms")]
            public bool UiForms { get; set; }
        }

        public partial class Scope
        {
            [JsonProperty("page_limit")]
            public long PageLimit { get; set; }
        }
    
}
