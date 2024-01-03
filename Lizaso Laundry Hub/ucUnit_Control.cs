using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lizaso_Laundry_Hub
{
    public partial class ucUnit_Control : UserControl
    {
        public Unit_Class Unit { get; private set; }
        public event EventHandler SelectButtonClicked;


        public ucUnit_Control(Unit_Class unit)
        {
            InitializeComponent();
            Unit = unit;
            ShowUnit();
        }

        public void ShowUnit()
        {
            int unitId = Unit.Unit_ID;
            int unitStatus = Unit.Avail_Status;
            label_unit.Text = Unit.Unit_Name;
            string reserved = Unit.Reserved;

            //Console.WriteLine($"Unit_Status: {Unit.Avail_Status}");
           //Console.WriteLine($"Unit_ID: {Unit.Unit_ID}");

            if (unitStatus == 0)
            {
                image_unit.Image = Properties.Resources.Available;
                btnReserved.Visible = false;
                btnSelect.Location = new Point(95, 301);
            }
            else if (unitStatus == 1)
            {
                image_unit.Image = Properties.Resources.Occupied;

                if(reserved == "Reserved")
                {
                    UnitReserved();
                }
                else
                {
                    UnitOccupied();
                }
            }
            else if (unitStatus == 2)
            {
              
            }
            else
            {
                //image_unit.Image = Properties.Resources.Washing_Reserved;
            }
        }


        public void UnitOccupied()
        {
            Color buttonColorOccupied1 = Color.FromArgb(245, 81, 95);
            Color buttonColorOccupied2 = Color.FromArgb(161, 5, 29);

            btnSelect.StateCommon.Back.Color1 = buttonColorOccupied1;
            btnSelect.StateCommon.Back.Color2 = buttonColorOccupied2;
            btnSelect.StateCommon.Border.Color1 = buttonColorOccupied1;
            btnSelect.StateCommon.Border.Color2 = buttonColorOccupied2;
            btnSelect.Values.Text = "Occupied";

            btnReserved.Visible = false;
            btnSelect.Location = new Point(95, 301);

            rbWash.Enabled = false;
            rbWashDry.Enabled = false;
            rbWashDryFold.Enabled = false;

            btnSelect.Enabled = false;
        }

        public void UnitReserved()
        {
            Color buttonColorOccupied1 = Color.FromArgb(245, 81, 95);
            Color buttonColorOccupied2 = Color.FromArgb(161, 5, 29);

            Color buttonColorReserved1 = Color.FromArgb(250, 217, 97);
            Color buttonColorReserved2 = Color.FromArgb(247, 143, 30);

            btnSelect.StateCommon.Back.Color1 = buttonColorOccupied1;
            btnSelect.StateCommon.Back.Color2 = buttonColorOccupied2;
            btnSelect.StateCommon.Border.Color1 = buttonColorOccupied1;
            btnSelect.StateCommon.Border.Color2 = buttonColorOccupied2;
            btnSelect.Values.Text = "Occupied";
            btnSelect.Location = new Point(25, 301);

            btnReserved.StateCommon.Back.Color1 = buttonColorReserved1;
            btnReserved.StateCommon.Back.Color2 = buttonColorReserved2;
            btnReserved.StateCommon.Border.Color1 = buttonColorReserved1;
            btnReserved.StateCommon.Border.Color2 = buttonColorReserved2;
            btnReserved.Values.Text = "Reserved";
            btnSelect.Location = new Point(25, 301);

            rbWash.Enabled = false;
            rbWashDry.Enabled = false;
            rbWashDryFold.Enabled = false;

            btnSelect.Enabled = false;
        }


        /*
        private void InitializeUI()
        {
            int unitId = Unit.Unit_ID;
            label_unit.Text = Unit.Unit_Name;
            Console.WriteLine($"Unit_ID: {Unit.Unit_ID}");
            Console.WriteLine($"Unit_Name: {Unit.Unit_Name}");


            if (Unit.Unit_Image != null && Unit.Unit_Image.Length > 0)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(Unit.Unit_Image))
                    {
                        image_unit.Image = Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (e.g., error converting byte array to image)
                    Console.WriteLine($"Error loading image: {ex.Message}");
                }
            }

            switch (Unit.Availability_Status)
            {
                case 1: // Assuming 1 represents "Wash" in the database
                    rbWash.Checked = true;
                    break;
                case 2: // Assuming 2 represents "Wash/Dry" in the database
                    rbWashDry.Checked = true;
                    break;
                case 3: // Assuming 3 represents "Wash/Dry/Fold" in the database
                    rbWashDryFold.Checked = true;
                    break;
                    // Handle other cases if needed
            }
        }
        */
        public string SelectedService()
        {
            if (rbWash.Checked)
            {
                return "Wash";
            }
            else if (rbWashDry.Checked)
            {
                return "Wash/Dry";
            }
            else if (rbWashDryFold.Checked)
            {
                return "Wash/Dry/Fold";
            }

            return string.Empty; // Return an empty string or handle default case as needed
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (rbWash.Checked || rbWashDry.Checked || rbWashDryFold.Checked)
            {

                // At least one radio button is checked
                string selectedService = SelectedService();

                Select_Unit_Form frm = new Select_Unit_Form(this.ParentForm as Services_Form);
                frm.txt_UnitName.Text = Unit.Unit_Name;
                frm.unitID = Unit.Unit_ID;
                frm.txt_Service.Text = selectedService;
                frm.ucUnit = this;
                SelectButtonClicked?.Invoke(this, EventArgs.Empty);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a service option before proceeding.");
            }
        }

        public void ClearSelection()
        {
            rbWash.Checked = false;
            rbWashDry.Checked = false;
            rbWashDryFold.Checked = false;
        }
    }
}
