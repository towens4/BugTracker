using BugTrackerUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BugTrackerUI.Controllers
{
    public class ErrorController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44379/api");
        private readonly HttpClient _httpClient;

        public ErrorController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<ErrorViewModel> errorList = new List<ErrorViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/error/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                errorList = JsonConvert.DeserializeObject<List<ErrorViewModel>>(data);
            }
            return View(errorList);
        }
    }
}
