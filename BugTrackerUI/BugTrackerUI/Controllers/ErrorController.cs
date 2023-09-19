using BugTrackerAPICall.APICall;
using BugTrackerAPICall.Helper;
using BugTrackerCore.Models;
using BugTrackerUICore.Helper;
using BugTrackerUICore.Models;
using BugTrackerUICore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json.Serialization;

namespace BugTrackerUI.Controllers
{
    public class ErrorController : Controller
    {
        public string applicationName = Assembly.GetExecutingAssembly().GetName().Name;
        private readonly IHttpClientFactory _httpClient;
        private readonly UserManager<IdentityUser> _userManager;
        public ErrorController(IHttpClientFactory httpClient, UserManager<IdentityUser> userManager)
        {
            _httpClient = httpClient;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
            
        }

        public async Task<IActionResult> ErrorList(Guid applicationId)
        {
            List<ErrorViewModel> errorList = new List<ErrorViewModel>();
            try
            {
                IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var userId = currentUser.Id;

                IdHolderModel idHolder = IdHolderFactory.CreateIdHolder(userId, applicationId);
                
                
                var errorApiList = await ApiCallFunctions.GetErrors(_httpClient, idHolder);

                if (errorApiList == null || !errorApiList.Any())
                    return View(errorList);

                errorList = ErrorFactory.CreateErrorList(errorApiList);
                
                
                return View(errorList);
            }
            catch (Exception ex)
            {
                await ApiCallFunctions.AddError(_httpClient, ex, applicationName, BugTrackerAPICall.Helper.CallerMethod.GetCallerMethodName());
                return View(errorList);
            }
        }

        public async Task<bool> ErrorTest()
        {
            try
            {
                object m = null;
                string s = m.ToString();

                return false;
            }
            catch(Exception ex)
            {

                await ApiCallFunctions.AddError(_httpClient, ex, applicationName, BugTrackerAPICall.Helper.CallerMethod.GetCallerMethodName()) ;
            }

            return true;
        }

        [HttpGet]
        public IActionResult CreateApplication()
        {
            BugTrackerUICore.Models.ViewModels.ApplicationViewModel application = new BugTrackerUICore.Models.ViewModels.ApplicationViewModel();
            return View(application);
        }

        public IActionResult CreateApplication(BugTrackerUICore.Models.ViewModels.ApplicationViewModel application)
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
