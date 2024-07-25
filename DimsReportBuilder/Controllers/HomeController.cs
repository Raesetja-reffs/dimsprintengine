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
using System.IO;

namespace Invoice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string apiUrl, string reportUrl)
        {
            if(apiUrl == null)
            {
                XtraReport errorReport = new ErrorReport();
                return View(errorReport);
            }

            if (apiUrl != null)
            {
                try
                {
                    if (reportUrl == null)
                    {
                        XtraReport rep = new OrderInvoice();
                        rep.RequestParameters = false;
                        rep.DataSource = CreateObjectDataSource(apiUrl);
                        //rep.CreateDocument();
                        //rep.CustomPageCount = rep.PrintingSystem.PageCount;
                        return View(rep);
                    }
                    else
                    {
                        // Fetch report file from API URL
                        var httpClient = _httpClientFactory.CreateClient();
                        var response = await httpClient.GetAsync(reportUrl);

                        if (!response.IsSuccessStatusCode)
                        {
                            return NotFound("Report not found or API error");
                        }

                        // Load the report from stream
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            XtraReport rep = XtraReport.FromStream(stream);
                            rep.RequestParameters = false;
                            rep.DataSource = CreateObjectDataSource(apiUrl);
                            //rep.CreateDocument();
                            //rep.CustomPageCount = rep.PrintingSystem.PageCount;
                            return View(rep);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    return BadRequest($"Error loading report: {ex.Message}");
                }
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
