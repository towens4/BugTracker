using BugTrackerUI.Helper;
using BugTrackerUI.Models;
using BugTrackerUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BugTrackerUI.Controllers
{
    public class ErrorController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44379/api");
        private readonly IHttpClientFactory _httpClient;

        public ErrorController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<ErrorViewModel> errorList = new List<ErrorViewModel>();

           

            return View(errorList);
        }

        [HttpGet]
        public IActionResult CreateApplication()
        {
            Application application = new Application();
            return View(application);
        }

        public IActionResult CreateApplication(Application application)
        {
            if(!ModelState.IsValid)
                return View();

            ApiHandler.AddApplication(_httpClient, application);

            return View();
        }
    }
}
