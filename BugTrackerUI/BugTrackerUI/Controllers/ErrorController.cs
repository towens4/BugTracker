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
            Models.ApplicationViewModel application = new Models.ApplicationViewModel();
            return View(application);
        }

        public IActionResult CreateApplication(Models.ApplicationViewModel application)
        {
            if(!ModelState.IsValid)
                return View();

            ApiHandler.AddApplication(_httpClient, application);

            return View();
        }

        [HttpPost("/Error/AddApplication")]
        public async Task<ActionResult> AddApplication(string data)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            /*IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            Application application = new Application() { ApplicationId = new Guid(), ApplicationName = data, UserId = user.Id };
            ApiHandler.AddApplication(_httpClient, application);*/
            //return View();
        }
    }
}
