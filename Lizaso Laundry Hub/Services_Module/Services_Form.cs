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

namespace Lizaso_Laundry_Hub
{
    public partial class Services_Form : KryptonForm
    {
        private RealTime_Data_Class realTime;
        private Get_Data_Class getData;
        
        private Select_Unit_Form selectUnitForm;

        public Services_Form()
        {
            InitializeComponent();
            getData = new Get_Data_Class();
            realTime = new RealTime_Data_Class(this);
            Load_Unit();
            
        }
        public void DisplayReserved()
        {
            if (tab_side.SelectedTab == tabPage2)
            {
                Load_Reserved();
            }
        }
        public async Task DisplayInProgress()
        {
            await realTime.LoadProgressAsync(flow_panel_progress);
        }

        private async void Services_Form_Load(object sender, EventArgs e)
        {
            try
            {
                await DisplayInProgress();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error na kailangan ayusin");
            }
        }

        private void UcUnitControl_SelectButtonClicked(object sender, EventArgs e)
        {
            this.Enabled = false;

            selectUnitForm = new Select_Unit_Form(this);
            selectUnitForm.FormClosed += SelectUnitForm_FormClosed;
            selectUnitForm.Show();
        }

        private void SelectUnitForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Enabled = true;

            selectUnitForm.Dispose();
        }
       
        public void Load_Unit()
        {
            try
            {
                flow_panel_unit.Controls.Clear();

                List<Unit_Class> units = getData.Get_RetrieveUnits();

                foreach (var unit in units)
                {
                    ucUnit_Control unitControl = new ucUnit_Control(unit);
                    flow_panel_unit.Controls.Add(unitControl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Load_Unit: {ex.Message}");
            }
        }

        public void Load_Reserved()
        {
            try
            {
                flow_panel_reserved.Controls.Clear();

                List<In_Reserved_Class> reserved = getData.Get_RetrieveLaundryBookingsReserved();

                foreach (var reve in reserved)
                {
                    ucReservedList_Control reservedControl = new ucReservedList_Control(reve);
                    flow_panel_reserved.Controls.Add(reservedControl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Load_Unit: {ex.Message}");
            }
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            Add_Reserved_Form frm = new Add_Reserved_Form(this);
            frm.ShowDialog();
        }

        private void tab_side_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayReserved();
        }

        private bool isPanel1Moved = false;

        private void btn_PanelMove_Click(object sender, EventArgs e)
        {
            if (isPanel1Moved)
            {
                splitContainer1.SplitterDistance = 960;
            }
            else
            {
                splitContainer1.SplitterDistance = splitContainer1.Width;
            }

            isPanel1Moved = !isPanel1Moved;
        }
    }
}
