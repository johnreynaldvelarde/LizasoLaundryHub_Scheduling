using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Lizaso_Laundry_Hub.Services_Module
{
    public partial class ReservedBy_PopUp_Form : KryptonForm
    {
        public int getUnitID;
        private Get_Data_Class getData;

        public ReservedBy_PopUp_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void DisplayReservedUI()
        {
            getData.Get_WhoCustomerReserved(getUnitID, Label_Customer, Label_StartTime, Label_EndTime);
        }

        private void ReservedBy_PopUp_Form_Load(object sender, EventArgs e)
        {
            DisplayReservedUI();
        }

        private void ReservedBy_PopUp_Form_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
