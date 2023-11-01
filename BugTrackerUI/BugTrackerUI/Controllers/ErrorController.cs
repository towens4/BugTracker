using BugTrackerAPICall.APICall;
using BugTrackerAPICall.Helper;
using BugTrackerCore;
using BugTrackerCore.Models;
using BugTrackerUICore.Helper;
using BugTrackerUICore.Models;
using BugTrackerUICore.Models.ViewModels;
using BugTrackerUICore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        private readonly IHttpMethods _httpMethods;
        public ErrorController(IHttpClientFactory httpClient, UserManager<IdentityUser> userManager, IHttpMethods httpMethods)
        {
            _httpClient = httpClient;
            _userManager = userManager;
            _httpMethods = httpMethods;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
            
        }

        [HttpGet]
        public async Task<IActionResult> ErrorContainer(Guid applicationId)
        {
            
            return View(applicationId);
        }

        /*[HttpGet]
        public async Task<IActionResult> PostUserId()
        {
            if (User.Identity == null)
                return NoContent();

            if (!User.Identity.IsAuthenticated)
            {
                //HttpContext.Session.Remove("CurrentUserId");
                return NoContent();
            }
            
            IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = currentUser.Id;

            var storedUserId = HttpContext.Session.GetString("CurrentUserId");

            if (string.IsNullOrEmpty(storedUserId) || storedUserId != userId)
            {
                _httpMethods.PostUserId(_httpClient, userId);
                //HttpContext.Session.SetString("CurrentUserId", userId);
            }

            return NoContent();
            
        }*/

        public async Task<IActionResult> ErrorList(Guid applicationId)
        {
            List<ErrorViewModel> errorList = new List<ErrorViewModel>();
            try
            {
                //IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var userId = HttpContext.Session.GetString("Id");
                //UserAccess.PostCurrentUser();
                //IdHolderModel idHolder = IdHolderFactory.CreateIdHolder(userId, applicationId);
                
                
                var errorApiList = await _httpMethods.GetErrors(_httpClient, applicationId);

                if (errorApiList == null || !errorApiList.Any())
                    return PartialView(errorList);

                errorList = ErrorFactory.CreateErrorList(errorApiList);
                
                
                return PartialView(errorList);
            }
            catch (Exception ex)
            {
                await _httpMethods.AddError(_httpClient, ex, applicationName, BugTrackerAPICall.Helper.CallerMethod.GetCallerMethodName());
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

                await _httpMethods.AddError(_httpClient, ex, applicationName, BugTrackerAPICall.Helper.CallerMethod.GetCallerMethodName()) ;
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

        public void UpdateCompletionStatus([FromBody] BugTrackerCore.Models.CompletedModel completedModel)
        {
            _httpMethods.UpdateCompletionStatus(_httpClient, completedModel);
        }


    }
}
