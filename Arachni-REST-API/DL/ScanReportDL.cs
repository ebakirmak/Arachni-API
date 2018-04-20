using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arachni_REST_API.DL
{
    public partial class ScanReportDL
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("seed")]
        public string Seed { get; set; }

        //[JsonProperty("options")]
        //public Options Options { get; set; }
        
        [JsonProperty("sitemap")]
        public Dictionary<string, long> Sitemap { get; set; }

        [JsonProperty("start_datetime")]
        public string StartDatetime { get; set; }

        [JsonProperty("finish_datetime")]
        public string FinishDatetime { get; set; }

        [JsonProperty("delta_time")]
        public DateTimeOffset DeltaTime { get; set; }

        [JsonProperty("issues")]
        public Issue[] Issues { get; set; }

        //[JsonProperty("plugins")]
        //public Plugins Plugins { get; set; }
    }

    public partial class Issue
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("references")]
        public Dictionary<string, string> References { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        
        [JsonProperty("severity")]
        public string Severity { get; set; }

        [JsonProperty("check")]
        public Check Check { get; set; }

        [JsonProperty("proof", NullValueHandling = NullValueHandling.Ignore)]
        public string Proof { get; set; }

        //[JsonProperty("vector")]
        //public Vector Vector { get; set; }

        //[JsonProperty("referring_page")]
        //public Page ReferringPage { get; set; }

        [JsonProperty("platform_name")]
        public object PlatformName { get; set; }

        [JsonProperty("platform_type")]
        public object PlatformType { get; set; }

        //[JsonProperty("page")]
        //public Page Page { get; set; }

        //[JsonProperty("remarks")]
        //public Remarks Remarks { get; set; }

        [JsonProperty("trusted")]
        public bool Trusted { get; set; }

        [JsonProperty("cwe", NullValueHandling = NullValueHandling.Ignore)]
        public long? Cwe { get; set; }

        [JsonProperty("remedy_guidance", NullValueHandling = NullValueHandling.Ignore)]
        public string RemedyGuidance { get; set; }

        [JsonProperty("cwe_url", NullValueHandling = NullValueHandling.Ignore)]
        public string CweUrl { get; set; }

        [JsonProperty("digest")]
        public long Digest { get; set; }

        //[JsonProperty("response")]
        //public Response Response { get; set; }

        //[JsonProperty("request")]
        //public Request Request { get; set; }
    }

    public partial class Check
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
