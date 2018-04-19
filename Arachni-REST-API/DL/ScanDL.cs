using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Arachni_REST_API.DL
{
    public class ScanDL
    {  
        [JsonProperty("url")]
        public object Url { get; set; }

        [JsonProperty("http")]
        public Http Http { get; set; }

        [JsonProperty("audit")]
        public Audit Audit { get; set; }

        [JsonProperty("input")]
        public Input Input { get; set; }

        [JsonProperty("browser_cluster")]
        public BrowserCluster BrowserCluster { get; set; }

        [JsonProperty("scope")]
        public Scope Scope { get; set; }

        [JsonProperty("session")]
        public Plugins Session { get; set; }

        [JsonProperty("checks")]
        public object[] Checks { get; set; }

        [JsonProperty("platforms")]
        public object[] Platforms { get; set; }

        [JsonProperty("plugins")]
        public Plugins Plugins { get; set; }

        [JsonProperty("no_fingerprinting")]
        public bool NoFingerprinting { get; set; }

        [JsonProperty("authorized_by")]
        public object AuthorizedBy { get; set; }
    }

    public partial class Audit
    {
        [JsonProperty("parameter_values")]
        public bool ParameterValues { get; set; }

        [JsonProperty("exclude_vector_patterns")]
        public object[] ExcludeVectorPatterns { get; set; }

        [JsonProperty("include_vector_patterns")]
        public object[] IncludeVectorPatterns { get; set; }

        [JsonProperty("link_templates")]
        public object[] LinkTemplates { get; set; }
    }

    public partial class BrowserCluster
    {
        [JsonProperty("wait_for_elements")]
        public Plugins WaitForElements { get; set; }

        [JsonProperty("pool_size")]
        public long PoolSize { get; set; }

        [JsonProperty("job_timeout")]
        public long JobTimeout { get; set; }

        [JsonProperty("worker_time_to_live")]
        public long WorkerTimeToLive { get; set; }

        [JsonProperty("ignore_images")]
        public bool IgnoreImages { get; set; }

        [JsonProperty("screen_width")]
        public long ScreenWidth { get; set; }

        [JsonProperty("screen_height")]
        public long ScreenHeight { get; set; }
    }

    public partial class Plugins
    {
    }

    public partial class Http
    {
        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }

        [JsonProperty("request_timeout")]
        public long RequestTimeout { get; set; }

        [JsonProperty("request_redirect_limit")]
        public long RequestRedirectLimit { get; set; }

        [JsonProperty("request_concurrency")]
        public long RequestConcurrency { get; set; }

        [JsonProperty("request_queue_size")]
        public long RequestQueueSize { get; set; }

        [JsonProperty("request_headers")]
        public Plugins RequestHeaders { get; set; }

        [JsonProperty("response_max_size")]
        public long ResponseMaxSize { get; set; }

        [JsonProperty("cookies")]
        public Plugins Cookies { get; set; }
    }

    public partial class Input
    {
        [JsonProperty("values")]
        public Plugins Values { get; set; }

        [JsonProperty("default_values")]
        public Dictionary<string, string> DefaultValues { get; set; }

        [JsonProperty("without_defaults")]
        public bool WithoutDefaults { get; set; }

        [JsonProperty("force")]
        public bool Force { get; set; }
    }

    public partial class Scope
    {
        [JsonProperty("redundant_path_patterns")]
        public Plugins RedundantPathPatterns { get; set; }

        [JsonProperty("dom_depth_limit")]
        public long DomDepthLimit { get; set; }

        [JsonProperty("exclude_path_patterns")]
        public object[] ExcludePathPatterns { get; set; }

        [JsonProperty("exclude_content_patterns")]
        public object[] ExcludeContentPatterns { get; set; }

        [JsonProperty("include_path_patterns")]
        public object[] IncludePathPatterns { get; set; }

        [JsonProperty("restrict_paths")]
        public object[] RestrictPaths { get; set; }

        [JsonProperty("extend_paths")]
        public object[] ExtendPaths { get; set; }

        [JsonProperty("url_rewrites")]
        public Plugins UrlRewrites { get; set; }
    }

  
    internal class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
