using AdvanceULB_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System;

namespace AdvanceULB_CMS.Controllers
{
    public class TaskController : Controller
    {

        HttpClient httpClient = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetAppDetailsDataList(string server,string appid)
        {
            List<AppDetailsData>? datalist = new();

            httpClient.BaseAddress = new Uri("http://localhost:23621/api");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("server", server);
            httpClient.DefaultRequestHeaders.Add("appid", appid);
            HttpResponseMessage rs = await httpClient.GetAsync(httpClient.BaseAddress + "/Task/GetAppDetailsData");

            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                AppDetails distResponse = JsonConvert.DeserializeObject<AppDetails>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;

                if (status == "success")
                {

                    datalist = distResponse.Data;

                    return View(datalist);
                }
            }

            return View(datalist);
        }


        //public IActionResult UpdateCount()
        //{ 
        //    return View(); 
        //}


        public async Task<IActionResult> Edit(int appid)
        {
            // Retrieve data from API
            PropertyCounts propertyCounts = new PropertyCounts();
            httpClient.BaseAddress = new Uri("http://localhost:23621/api");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("server", propertyCounts.server);
            HttpResponseMessage rs = await httpClient.GetAsync(httpClient.BaseAddress + $"/ULB/GetULB/{appid}");
          
            if (!rs.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var responseString = await rs.Content.ReadAsStringAsync();
            var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
            PropertyCounts distResponse = JsonConvert.DeserializeObject<PropertyCounts>(responseString);

            var code = (int)rs.StatusCode;
            var status = dynamicobject.status.ToString();
            var message = dynamicobject.message.ToString();
            var data = dynamicobject.data;

            if (status == "success")
            {
                data = distResponse;
                return View(data);
            }

            

            // Pass data to the view
            return View(data);
        }

        // POST: /Your/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PropertyCounts model)
        {
            if (id != model.appId)
            {
                return BadRequest();
            }

            // Update data via API
            var response = await httpClient.PutAsJsonAsync($"your_api_endpoint/{id}", model);
            if (!response.IsSuccessStatusCode)
            {
                // Handle error
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
}
