using DevExpress.Pdf.Native.BouncyCastle.Ocsp;
using DevExpress.XtraExport.Implementation;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace DimsReportBuilder.Reports
{
    public partial class OrderInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        //public int CustomPageCount = -1;
        public OrderInvoice()
        {
            InitializeComponent();
        }
        private void PageFooter_BeforePrint(object sender, CancelEventArgs e)
        {
        }

        private void BottomMargin_BeforePrint(object sender, CancelEventArgs e)
        {
            //xrTable4.Visible = true;
            //xrLabel12.Visible = false;
        }

        private void xrLabel12_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //if (CustomPageCount != -1)
            //{
            //    if (CustomPageCount == 1 || (CustomPageCount == (e.PageIndex + 1)))
            //    {
            //        xrLabel12.Visible = false;
            //    }
            //    else
            //    {
            //        xrLabel12.Visible = true;
            //    }
            //}
        }

        private void xrTable4_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            //if (CustomPageCount != -1)
            //{
            //    if (CustomPageCount == (e.PageIndex + 1))
            //    {
            //        xrTable4.Visible = true;
            //    }
            //    else
            //    {
            //        xrTable4.Visible = false;
            //    }
            //}
        }
    }
}
