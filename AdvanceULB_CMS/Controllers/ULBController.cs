using AdvanceULB_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
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
    

        [HttpPost]
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

                    return View(ulb);
                }
            }

            return View(ulb);
        }



        [HttpGet]
        public IActionResult CreateULB()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateULB(ULBCreation ulbData)
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(ulbData);
                httpClient.BaseAddress = new Uri("http://localhost:23621/api");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("server", ulbData.server);

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

    }
}
