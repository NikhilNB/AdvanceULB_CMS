using AdvanceULB_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace AdvanceULB_CMS.Controllers
{
    public class ULBController : Controller
    {
        private readonly HttpClient _httpClient;
        public readonly DatabaseContext _dbContext;

        public ULBController()
        {
            _dbContext = new DatabaseContext();
            _httpClient = new HttpClient();
        }

        //Method to select server for GetULBs method
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetULBs(string server)
        {
            List<PropertyCounts>? list = new List<PropertyCounts>();
            _httpClient.BaseAddress = new Uri("http://localhost:23621/api");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("server", server);
            HttpResponseMessage rs = await _httpClient.GetAsync(_httpClient.BaseAddress + "/ULB/GetULB");
            
            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                PropertyCounts distResponse = JsonConvert.DeserializeObject<PropertyCounts>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;

                if (status == "success")
                {
                    list = distResponse.Data;
                    return View(list);
                }
            }

            return View(list);
        }

        //Method to select server for GetSTD method
        [HttpGet]
        public IActionResult STDGet()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetSTD(string server)
        {
            List<STDModel>? list = new List<STDModel>();
            _httpClient.BaseAddress = new Uri("http://localhost:23621/api");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("server", server);
            HttpResponseMessage rs = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Task/GetSTD");

            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                STDModel distResponse = JsonConvert.DeserializeObject<STDModel>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;

                if (status == "success")
                {
                    list = distResponse.Data;
                    return View(list);
                }
            }

            return View(list);
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
                _httpClient.BaseAddress = new Uri("http://localhost:23621/api");
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("server", ulbData.server);

                HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/ULB/CreateULB",
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

        //Method to get server and appid for GetAppDetailsDataList
        [HttpGet]
        public IActionResult GetFields()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAppDetailsDataList(string server, string appId)
        {
            List<PropertyCounts>? list = new List<PropertyCounts>();
            string apiUrl = $"http://localhost:23621/api/Task/GetAppDetailsData?server={server}&appId={appId}";
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("server", server);
            _httpClient.DefaultRequestHeaders.Add("appid", appId);
            HttpResponseMessage rs = await _httpClient.GetAsync(apiUrl);

            if (rs.IsSuccessStatusCode)
            {
                var responseString = await rs.Content.ReadAsStringAsync();
                var dynamicobject = JsonConvert.DeserializeObject<dynamic>(responseString);
                PropertyCounts distResponse = JsonConvert.DeserializeObject<PropertyCounts>(responseString);

                var code = (int)rs.StatusCode;
                var status = dynamicobject.status.ToString();
                var message = dynamicobject.message.ToString();
                var data = dynamicobject.data;

                if (status == "success")
                {

                    list = distResponse.Data;

                    return View(list);
                }
            }

            return View(list);
        }

        [HttpGet]
        public IActionResult Update(PropertyCounts obj)
        {
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PropertyCounts obj, int appId, string server)
        {
            try
            {
                _httpClient.BaseAddress = new Uri("http://localhost:23621/api");
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("appid", obj.appId.ToString());
                _httpClient.DefaultRequestHeaders.Add("server", obj.server);
                HttpResponseMessage rs = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + $"/Task/UpdateData/", obj);

                if (rs.IsSuccessStatusCode)
                {
                    var responseData = await rs.Content.ReadAsStringAsync();
                    var updatedData = JsonConvert.DeserializeObject<PropertyCounts>(responseData);
                    return RedirectToAction("Index", "ULB");
                }
                else
                {
                    string errorMessage = await rs.Content.ReadAsStringAsync();
                    return View("Error", new ErrorViewModel { Message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel { Message = ex.Message };
                return View("Error", errorViewModel);
            }
        }

        [HttpGet]
        public IActionResult UpdateSTD(UpdateSTDModel obj)
        {
            return View(obj);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSTD(UpdateSTDModel obj, int appId, string server)
        {
            try
            {
                obj.appId = appId;
                obj.server = server;
                _httpClient.BaseAddress = new Uri("http://localhost:23621/api");
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonContent = JsonConvert.SerializeObject(obj);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage rs = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + $"/Task/UpdateSTD", obj);
                if (rs.IsSuccessStatusCode)
                {
                    var responseData = await rs.Content.ReadAsStringAsync();
                    var updatedData = JsonConvert.DeserializeObject<STDModel>(responseData);
                    return RedirectToAction("GetSTD", "ULB");
                }
                else
                {
                    string errorMessage = await rs.Content.ReadAsStringAsync();
                    return View("Error", new ErrorViewModel { Message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                var errorViewModel = new ErrorViewModel { Message = ex.Message };
                return View("Error", errorViewModel);
            }
        }

    }
}
