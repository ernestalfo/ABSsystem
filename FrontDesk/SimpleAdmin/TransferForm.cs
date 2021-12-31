using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleAdmin
{
    public partial class TransferForm : Form
    {
        int laneID;

        public int laneIDresult
        {
            get
            {
                return laneID;
            }
        }
        public TransferForm()
        {
            InitializeComponent();
        }

        public void SetLanes(String[] laneslist)
        {
            if (laneslist != null)
            {
                LanesTargetCBox.Items.Clear();
                LanesTargetCBox.Items.AddRange(laneslist);
                LanesTargetCBox.SelectedIndex = 0;
                string pista = (string)LanesTargetCBox.Items[LanesTargetCBox.SelectedIndex];
                pista = pista.Substring(6);
                Int32.TryParse(pista, out laneID);
            }
        }

        private void LanesTargetCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pista = (string)LanesTargetCBox.Items[LanesTargetCBox.SelectedIndex];
            pista = pista.Substring(6);
            Int32.TryParse(pista, out laneID);
        }

        public bool LeftDesactivate()
        {
            return DesactivateChckB.Checked; 
        }
    }//TransferForm
}
