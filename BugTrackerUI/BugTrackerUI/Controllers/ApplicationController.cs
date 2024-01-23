using BugTrackerUICore.Helper;
using BugTrackerUICore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BugTrackerUI.Controllers
{
    public class ApplicationController : Controller
    {
        IHttpClientFactory _httpClient;
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationController(IHttpClientFactory httpClient, UserManager<IdentityUser> userManager)
        {
            _httpClient = httpClient;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            try
            {
                
                return View();
            }
            catch (Exception ex)
            {
                ApiHandler.AddError(_httpClient, ex);
                //throw;
            }

            return View();
        }

        //[Route("Application/AddApplication/applicationName")]
        [HttpPost("/Application/AddApplication")]
        public async Task<ActionResult> AddApplication([FromBody] ApplicationNameInputModel applicationName)
        {
            try
            {
                var appiName = Assembly.GetExecutingAssembly().GetName().Name;
                string appName = applicationName.ApplicationName;
                string userId = HttpContext.Session.GetString("Id");
                ApplicationViewModel application = new ApplicationViewModel() { ApplicationId = new Guid(), ApplicationName = appName, UserId = userId };
                ApiHandler.AddApplication(_httpClient,application);
                return Ok();
            }
            catch(Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
            /**/
            //return View();
        }

        [HttpPost]
        public IActionResult AddApplicationTest()
        {
            
            return Ok("Success");
        }

        public IActionResult RefreshApplicationList()
        {
            return ViewComponent("Application");
        }

        public IActionResult NoApplication()
        {
            return View();
        }
    }
}
