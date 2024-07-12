using DevExpress.XtraExport.Implementation;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace DimsReportBuilder.Reports
{
    public partial class DeliveryReport : DevExpress.XtraReports.UI.XtraReport
    {
        public DeliveryReport()
        {
            InitializeComponent();
        }
        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void BottomMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            xrTable4.Visible = true;
            xrLabel12.Visible = false;
        }
    }
}
