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
         * Yeni bir Scan Oluşturur.
         * 
         */
        public string CreateScan(ArachniManager manager, ScanCreateDL scan)
        {
            string json = JsonConvert.SerializeObject(scan);
            string id = manager.POSTScanCreate(json);
            return id;
        }

        /*
         * Scan duraklat
         * 
         */
         public string PauseScan(ArachniManager manager, string id)
        {
            try
            {
                return manager.PUTPauseScan(id);
            }
            catch (Exception ex)
            {

                throw ex;
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
                if (responseBody == "Unauthorized" || responseBody == null)
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

                Console.WriteLine("ScanBL::GETScanID\n" + ex.Message);
                return null;
            }

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
         * Servisin çalışıp çalışmadığını kontrol eder.
         * 
         */
        public bool ServiceControl(ArachniManager manager)
        {
            return manager.GetServiceControl();
        }
              

     
        /*
         * id değerini json ifadeden al ve geri döndür.
         * 
         */
         public string GetID(string id)
        {
            if (id.Contains("id"))
            {
                ScanCreateResponseDL scanCreateResponseDL = JsonConvert.DeserializeObject<ScanCreateResponseDL>(id);
                id = scanCreateResponseDL.ID;
            }
            return id;
        }

  
    }
}
