using DevExpress.XtraReports.UI;
using DimsReportBuilder.Reports;
using Invoice.Models;
using Invoice.Reports;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.Web;
using DevExpress.DataAccess.Json;
using System.Collections.Specialized;

namespace Invoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string apiUrl)
        {
            if(apiUrl == null)
            {
                XtraReport errorReport = new ErrorReport();
                return View(errorReport);
            }

            Uri uri = new Uri(apiUrl);
            NameValueCollection queryParameters = HttpUtility.ParseQueryString(uri.Query);

            if (queryParameters["is_without_price"] != null)
            {
                string updatedUrl = RemoveParametersFromUrl(apiUrl, "is_without_price");

                XtraReport rep = new DeliveryReport();
                rep.RequestParameters = false;
                rep.DataSource = CreateObjectDataSource(updatedUrl);
                return View(rep);
            }

            if (apiUrl != null && queryParameters["is_without_price"] == null)
            {
                var rep = new OrderInvoice();
                rep.RequestParameters = false;
                rep.DataSource = CreateObjectDataSource(apiUrl);
                //rep.CreateDocument();
                //rep.CustomPageCount = rep.PrintingSystem.PageCount;
                return View(rep);
            }
            XtraReport report = new ErrorReport();
            return View(report);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private XtraReport CreateReport()
        {
            XtraReport report = new OrderInvoice();
            report.RequestParameters = false;
            return report;
        }

        private object CreateObjectDataSource(string url)
        {
            JsonDataSource jsonDataSource = new JsonDataSource();
            try
            {
                string apiUrl = url;
                var client = new RestClient(apiUrl);
                var request = new RestRequest();
                request.Method = Method.Get;
                RestResponse response = client.Execute(request);
                string jsonString = "";
                if (response.IsSuccessStatusCode)
                {
                    string? jsonFromApi = response.Content;
                    var data = JsonConvert.DeserializeObject<Rootobject>(jsonFromApi);
                    jsonString = JsonConvert.SerializeObject(data);
                }
                CustomJsonSource customJsonSource = new CustomJsonSource();
                customJsonSource.Json = jsonString;
                jsonDataSource.ConnectionName = null;
                jsonDataSource.JsonSource = customJsonSource;
                jsonDataSource.Name = "jsonDataSource1";
                jsonDataSource.Fill();
                return jsonDataSource;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return jsonDataSource;
        }

        public string RemoveParametersFromUrl(string url, params string[] parametersToRemove)
        {
            if (string.IsNullOrEmpty(url)) return url;
            Uri uri = new Uri(url);
            string baseUrl = uri.GetLeftPart(UriPartial.Path);
            string queryString = uri.Query;
            if (string.IsNullOrEmpty(queryString)) return url;
            var query = HttpUtility.ParseQueryString(queryString);
            foreach (string parameterToRemove in parametersToRemove)
            {
                query.Remove(parameterToRemove);
            }
            string updatedQueryString = query.ToString();
            string updatedUrl = baseUrl + (string.IsNullOrEmpty(updatedQueryString) ? "" : "?" + updatedQueryString);
            return updatedUrl;
        }
    }
}
