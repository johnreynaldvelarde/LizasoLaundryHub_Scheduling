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
using Microsoft.Reporting.WinForms;

namespace Lizaso_Laundry_Hub.Receipt_Module
{
    public partial class Receipt_Form : KryptonForm
    {
        public Receipt_Form()
        {
            InitializeComponent();
        }

        private void Receipt_Form_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        public bool GetPaymentDetails(string _userName, string _customerName, string _serviceType)
        {

            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("p_StaffName", _userName);
            parameters[1] = new ReportParameter("p_CustomerName", _customerName);
            parameters[2] = new ReportParameter("p_ServiceType", _serviceType);
            parameters[3] = new ReportParameter("p_Date", DateTime.Now.ToShortDateString());


            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();

            return true;
        }

        public bool Get_AdditonalPayment(string _userName, string _customerName, string _serviceType, List<Item_Data> additionalItems)
        {
            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("p_StaffName", _userName);
            parameters[1] = new ReportParameter("p_CustomerName", _customerName);
            parameters[2] = new ReportParameter("p_ServiceType", _serviceType);
            parameters[3] = new ReportParameter("p_Date", DateTime.Now.ToShortDateString());

            // Clear existing data sources
            reportViewer1.LocalReport.DataSources.Clear();

            // Set the additionalItems as a report data source
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ds", additionalItems));

            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
            return true;
        }
    }
}
