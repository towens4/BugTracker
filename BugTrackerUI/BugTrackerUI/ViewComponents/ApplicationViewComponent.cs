using BugTrackerUI.Models;
using BugTrackerUI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BugTrackerUI.Helper;
using System.Reflection;
using System.Net.Http;
using BugTrackerAPICall.APICall;

namespace BugTrackerUI.ViewComponents
{
    public class ApplicationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        public ApplicationViewComponent(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {


            try
            {
                //throw new Exception();
                IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var userId = currentUser.Id;
                List<ApplicationViewModel> applications = null;
                /*if (await ApiHandler.PostAppName(_httpClientFactory, Assembly.GetExecutingAssembly().GetName().Name))
                {
                    applications = await ApiHandler.GetApplications(_httpClientFactory, userId);
                }*/
                if (await ApiCallFunctions.PostAppName(_httpClientFactory, Assembly.GetExecutingAssembly().GetName().Name))
                {
                    applications = new List<ApplicationViewModel>();
                    var apiList = await ApiCallFunctions.GetApplications(_httpClientFactory, userId);
                    foreach (var app in apiList)
                    {
                        applications.Add(new ApplicationViewModel()
                        {
                            ApplicationId = app.ApplicationId,
                            ApplicationName = app.ApplicationName,
                            UserId = app.UserId,
                        });
                    }
                    //applications = apiList.ToList();
                }
                //IEnumerable<ApplicationViewModel> applications = await ApiHandler.GetApplications(_httpClientFactory, userId);

                if (applications == null)
                    return View(new List<ApplicationViewModel>());

                return View(applications);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            

            return View();
        }
    }
}
