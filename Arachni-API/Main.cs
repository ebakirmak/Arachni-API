using Arachni;
using Arachni_API.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arachni_API
{
    public partial class Main : Form
    {
        private ArachniSession session;

        private ArachniManager manager;
        public Main()
        {
            using (session = new ArachniSession("206.189.12.255", 443))
            {
                using (manager = new ArachniManager(session))
                {
                  
                }
            }
            InitializeComponent();
        }

        private  void Main_Load(object sender, EventArgs e)
        {
          
        }

        private void btnScans_Click(object sender, EventArgs e)
        {
            ScanBL scan = new ScanBL();
            foreach (string id in scan.GETScanID(manager))
            {
                lstScansID.Items.Add(id);
            }
        }
    }
}
