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

        public bool GetPaymentDetails(string _userName, string _serviceType, string _load, string _weight, string _amount, string _totalAmount, string _customerName, string _paymentMethod, string _address)
        {

            ReportParameter[] parameters = new ReportParameter[10];
            parameters[0] = new ReportParameter("StaffName", _userName);
            parameters[1] = new ReportParameter("Date", DateTime.Now.ToShortDateString());
            parameters[2] = new ReportParameter("ServicesType", _serviceType);
            parameters[3] = new ReportParameter("Load", _load);
            parameters[4] = new ReportParameter("Weight", _weight);
            parameters[5] = new ReportParameter("Amount", _amount);
            parameters[6] = new ReportParameter("TA", _totalAmount);
            parameters[7] = new ReportParameter("CustomerName", _customerName);
            parameters[8] = new ReportParameter("PaymentMethod", _paymentMethod);
            parameters[9] = new ReportParameter("Address", _address);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
