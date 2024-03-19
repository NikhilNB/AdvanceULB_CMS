using AdvanceULB_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AdvanceULB_CMS.Controllers
{
    public class ULBController : Controller
    {
        HttpClient httpClient = new HttpClient();
        

        public IActionResult Index()
        {
            return View();
          
        }

        //dfgsdfgsf

        [HttpGet]
        public IActionResult GetServer()
        {
            return View();
        }

        public IActionResult GetServer(string server)
        {
            Servers model = new Servers();
            model.SelectedServer = server;
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> GetULBs(string server)
        {
            List<ULBData>? ulb = new();

            httpClient.BaseAddress = new Uri("http://localhost:23621/api");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("server", server);
            HttpResponseMessage rs = await httpClient.GetAsync(httpClient.BaseAddress + "/ULB/GetULB");

            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                ULB distResponse = JsonConvert.DeserializeObject<ULB>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;

                if (status == "success")
                {
                    ulb = distResponse.Data;
                    string formattedJson = JsonConvert.SerializeObject(ulb, Formatting.Indented);

                    return Content(formattedJson, "application/json");
                }
            }

            return View(ulb);
        }

        [HttpGet]
        public IActionResult CreateULB(string server)
        {
            var ulbModel = new ULBCreation();
            return View(ulbModel);
        }

        
        public async Task<IActionResult> CreateULB(ULBCreation ulbData, string server)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(ulbData);
                httpClient.BaseAddress = new Uri("http://localhost:23621/api");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("server", server);

                HttpResponseMessage response = await httpClient.PostAsync(httpClient.BaseAddress + "/ULB/CreateULB",
                                                                          new StringContent(jsonData, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return Ok("ULB created successfully!");
                }
                else
                {
                    return BadRequest("Failed to create ULB.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating ULB: " + ex.Message);
            }
        }


        [HttpPost]
        public ActionResult PerformAction(string server, string action)
        {
            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(action))
            {
                return View("Error");
            }

            switch (action)
            {
                case "get":
                    return RedirectToAction("GetULBs", new { server = server });
                case "create":
                    return RedirectToAction("CreateULB", new { server = server });
                default:
                    return View("Error");
            }
        }


    }
}
