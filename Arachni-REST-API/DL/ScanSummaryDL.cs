using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arachni_REST_API.DL
{
    public class ScanSummaryDL
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("busy")]
        public bool Busy { get; set; }

        [JsonProperty("seed")]
        public string Seed { get; set; }

        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }

        [JsonProperty("messages")]
        public string[] Messages { get; set; }
    }

    //public partial class Statistics
    //{
    //    [JsonProperty("http")]
    //    public Http Http { get; set; }

    //    [JsonProperty("browser_cluster")]
    //    public BrowserCluster BrowserCluster { get; set; }

    //    [JsonProperty("runtime")]
    //    public double Runtime { get; set; }

    //    [JsonProperty("found_pages")]
    //    public long FoundPages { get; set; }

    //    [JsonProperty("audited_pages")]
    //    public long AuditedPages { get; set; }

    //    [JsonProperty("current_page")]
    //    public string CurrentPage { get; set; }
    //}

    //public partial class BrowserCluster
    //{
    //    [JsonProperty("seconds_per_job")]
    //    public double SecondsPerJob { get; set; }

    //    [JsonProperty("total_job_time")]
    //    public long TotalJobTime { get; set; }

    //    [JsonProperty("queued_job_count")]
    //    public long QueuedJobCount { get; set; }

    //    [JsonProperty("completed_job_count")]
    //    public long CompletedJobCount { get; set; }

    //    [JsonProperty("time_out_count")]
    //    public long TimeOutCount { get; set; }
    //}

    //public partial class Http
    //{
    //    [JsonProperty("request_count")]
    //    public long RequestCount { get; set; }

    //    [JsonProperty("response_count")]
    //    public long ResponseCount { get; set; }

    //    [JsonProperty("time_out_count")]
    //    public long TimeOutCount { get; set; }

    //    [JsonProperty("total_responses_per_second")]
    //    public double TotalResponsesPerSecond { get; set; }

    //    [JsonProperty("burst_response_time_sum")]
    //    public double BurstResponseTimeSum { get; set; }

    //    [JsonProperty("burst_response_count")]
    //    public long BurstResponseCount { get; set; }

    //    [JsonProperty("burst_responses_per_second")]
    //    public double BurstResponsesPerSecond { get; set; }

    //    [JsonProperty("burst_average_response_time")]
    //    public double BurstAverageResponseTime { get; set; }

    //    [JsonProperty("total_average_response_time")]
    //    public double TotalAverageResponseTime { get; set; }

    //    [JsonProperty("max_concurrency")]
    //    public long MaxConcurrency { get; set; }

    //    [JsonProperty("original_max_concurrency")]
    //    public long OriginalMaxConcurrency { get; set; }
    //}

  

   
}
