namespace Invoice.Models
{
    public class Rootobject
    {
        public Orderheader[] orderheader { get; set; }
        public Companyinfo[] companyInfo { get; set; }
        public Orderline[] orderlines { get; set; }
    }

    public class Orderheader
    {
        public string CustomerNumber { get; set; }
        public string SoldTo { get; set; }
        public string ShipTo { get; set; }
        public string DocNumber { get; set; }
        public string DocDate { get; set; }
        public object DIMS_OrderNo { get; set; }
        public string strCurrency { get; set; }
        public string subtotal { get; set; }
        public string Total { get; set; }
        public string tax { get; set; }
        public string CustomerPastelCode { get; set; }
        public string PaymentTerms { get; set; }
        public string InvDiscAmnt { get; set; }
        public string strVAT { get; set; }
        public string strSalesPerson { get; set; }
    }

    public class Companyinfo
    {
        public string intAutoReportID { get; set; }
        public string strHtmlHeader { get; set; }
        public string Company { get; set; }
        public string MainAddress { get; set; }
        public string Suburb { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string RegNo { get; set; }
        public string VATNO { get; set; }
        public string AccountHolder { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string strFormName { get; set; }
        public DateTime dtm { get; set; }
        public string intOwnerID { get; set; }
        public string strHtmlFooter { get; set; }
        public string strLogoPath { get; set; }
        public object strQuoteMessage { get; set; }
        public string strDisclaimer { get; set; }
        public string strCompanyLogoName { get; set; }
    }

    public class Orderline
    {
        public string OrderId { get; set; }
        public object DocNumber { get; set; }
        public string PartNumber { get; set; }
        public string qty { get; set; }
        public string UnitOfMeasure { get; set; }
        public string UnitPrice { get; set; }
        public string LineTax { get; set; }
        public string LineTotal { get; set; }
        public string DIMS_OrderDetailID { get; set; }
        public string PDesc { get; set; }
        public string LineDiscount { get; set; }
        public string Location { get; set; }
        public object UserDef1 { get; set; }
        public object UserDef2 { get; set; }
        public object UserDef3 { get; set; }
        public int DisplayLine { get; set; }
    }
}
