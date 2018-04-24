using Arachni;
using Arachni_REST_API.DL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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

        /*
         * Bu fonksiyon Arachni cevabından gelen taramaların id'lerini Listeye ekler.
         * This function insert value id  of scans where from Arachni response.
         */
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

        /*
         * ListScan listesini döndürür.
         * ListScan list return.
         */
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

        /*
         * Gelen Scan ID'leri JSON'dan ayırarak ListScan'a ekler.
         * 
         */
        public List<string> ScanID(ArachniManager manager)
        {
            try
            {
                string responseBody = manager.GetScans();
                if (responseBody == "Unauthorized")
                {
                    return null;
                }
                else
                {
                    JObject jObject = JObject.Parse(responseBody);
                    this.ListScan = new List<string>();
                    foreach (JProperty property in jObject.Properties())
                    {
                        setListScan(property.Name);
                    }

                    return getListScan();
                }
               
            }
            catch (Exception ex)
            {

                Console.WriteLine("ScanBL::GETScanID\n"+ex.Message);
                return null;
            }
            
        }

        /*
         * 
         * Monitor scan progress
         */
        public ScanMonitorDL ScanMonitor (ArachniManager manager,string id)
        {
           
            string ScanMonitorJson = manager.GetScanMonitor(id);
            ScanMonitorDL scanDL;
            if (ScanMonitorJson!=null)
                scanDL  = JsonConvert.DeserializeObject<ScanMonitorDL>(ScanMonitorJson);
            else
                return null;

            return scanDL;
        }


        /*
         * 
         * Returns the same data as "Monitor scan progress" but without issues, errors and sitemap
         */
        public ScanSummaryDL ScanSummary(ArachniManager manager,string id)
        {
            

            ScanSummaryDL scanDL = JsonConvert.DeserializeObject<ScanSummaryDL>(manager.GetScanSummary(id));
            return scanDL;
        }

        /*
         * 
         * Retrieve a scan report
         */
        public ScanReportDL ScanReport(ArachniManager manager,string id)
        {

            string report = manager.GetScanReport(id, "json");
            ScanReportDL scanReportDL = JsonConvert.DeserializeObject<ScanReportDL>(report);
            SaveReport(report);
            //report = manager.GetScanReport(id, "xml");
            //SaveReport(report);
            
            return scanReportDL;
        }

    

        /*
         * Yeni bir Scan Oluşturur.
         * 
         */ 
         public string CreateScan(ArachniManager manager, ScanCreateDL scan)
        {
            string json = JsonConvert.SerializeObject(scan);
            string id = manager.SetScanCreate(json);
            return id;
        }

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

        /*
         * Servisin çalışıp çalışmadığını kontrol eder.
         * 
         */
        public bool ServiceControl(ArachniManager manager)
        {
            return manager.GetServiceControl();
        }


        /*
         * Tarama Raporunu Json'a çevirir ve kaydeder. Masaüstüne.
         * 
         */
       

       

        /*
         * Raporları Kaydet.
         * 
         */
        private bool SaveReport(string json)
        {
            try
            {
                string strPath = Environment.GetFolderPath(
                           System.Environment.SpecialFolder.DesktopDirectory);                
                System.IO.File.WriteAllText(strPath + "\\Aranchi.txt", json.ToString());
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;             
            }
           
        }

  
    }
}
