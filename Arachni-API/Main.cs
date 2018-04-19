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
        public Main()
        {
            InitializeComponent();
        }

        private  void Main_Load(object sender, EventArgs e)
        {
            using (ArachniSession session = new ArachniSession("206.189.12.255", 443))
            {
                using (ArachniManager manager = new ArachniManager(session))
                {
                    Scan scan = new Scan();
                    MessageBox.Show(scan.GETScanID(manager).ToString());
                }
            }
        }

        
    }
}
