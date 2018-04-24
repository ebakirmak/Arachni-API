using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arachni_REST_API.BL
{
    /*
     * Bu sınıf checks (policy) ile ilgili işlemlerin yapıldığı sınıftır.
     * 
     */ 
    public class ChecksBL
    {
        /*
     * Checks (Policy) Listele ve Oluştur
     * 
     */
        public List<String> ListChecks()
        {
            List<String> ListCheck = new List<String>();
            //ActiveChecks;
            ListCheck.Add("sql_injection");
            ListCheck.Add("sql_injection_differential");
            ListCheck.Add("sql_injection_timing");
            ListCheck.Add("no_sql_injection");
            ListCheck.Add("no_sql_injection_differential");
            ListCheck.Add("csrf");
            ListCheck.Add("code_injection");
            ListCheck.Add("code_injection_timing");
            ListCheck.Add("ldap_injection");
            ListCheck.Add("path_traversal");
            //ListCheck.Add("file_inclusion)");
            ListCheck.Add("response_splitting");
            ListCheck.Add("os_cmd_injection");
            ListCheck.Add("os_cmd_injection_timing");
            ListCheck.Add("rfi");
            ListCheck.Add("unvalidated_redirect");
            ListCheck.Add("unvalidated_redirect_dom");
            ListCheck.Add("xpath_injection");
            ListCheck.Add("xss");
            ListCheck.Add("xss_path");
            ListCheck.Add("xss_event");
            ListCheck.Add("xss_tag");
            ListCheck.Add("xss_script_context");
            ListCheck.Add("xss_dom");
            ListCheck.Add("xss_dom_script_context");
            ListCheck.Add("source_code_disclosure");
            ListCheck.Add("xxe");
            //Passive Checks
            ListCheck.Add("allowed_methods");
            ListCheck.Add("backup_files");
            ListCheck.Add("backup_directories");
            ListCheck.Add("common_admin_interfaces");
            ListCheck.Add("common_directories");
            ListCheck.Add("common_files");
            ListCheck.Add("http_put");
            //ListCheck.Add("unencrypted_password_form");
            ListCheck.Add("webdav");
            ListCheck.Add("xst");
            ListCheck.Add("credit_card");
            ListCheck.Add("cvs_svn_users");
            ListCheck.Add("private_ip");
            ListCheck.Add("backdoors");
            ListCheck.Add("htaccess_limit");
            ListCheck.Add("interesting_responses");
            ListCheck.Add("html_objects");
            ListCheck.Add("emails");
            ListCheck.Add("ssn");
            ListCheck.Add("directory_listing");
            ListCheck.Add("mixed_resource");
            ListCheck.Add("insecure_cookies");
            ListCheck.Add("http_only_cookies");
            ListCheck.Add("password_autocomplete");
            ListCheck.Add("origin_spoof_access_restriction_bypass");
            ListCheck.Add("form_upload");
            ListCheck.Add("localstart_asp");
            ListCheck.Add("cookie_set_for_parent_domain");
            ListCheck.Add("hsts");
            ListCheck.Add("x_frame_options");
            ListCheck.Add("insecure_cors_policy");
            ListCheck.Add("insecure_cross_domain_policy_access");
            ListCheck.Add("insecure_cross_domain_policy_headers");
            ListCheck.Add("insecure_client_access_policy");

            return ListCheck;

        }
    }
}
