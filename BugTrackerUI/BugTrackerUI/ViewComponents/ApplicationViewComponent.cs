using BugTrackerUICore.Models;
using BugTrackerUICore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BugTrackerUICore.Helper;
using System.Reflection;
using System.Net.Http;
using BugTrackerAPICall.APICall;
using Microsoft.Extensions.Caching.Distributed;
using BugTrackerUICore.Interfaces;
using BugTrackerAPICall.Interfaces;

namespace BugTrackerUI.ViewComponents
{
    public class ApplicationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpMethods _httpMethods;
        private readonly ICachingService _cachingService;
        public ApplicationViewComponent(ApplicationDbContext context, UserManager<IdentityUser> userManager, 
            IHttpClientFactory httpClientFactory, IHttpMethods httpMethods, ICachingService cachingService)
        {
            _context = context;
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
            _httpMethods = httpMethods;
            _cachingService = cachingService;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {


            try
            {
                IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var userId = currentUser.Id;
                List<ApplicationViewModel> applications = new List<ApplicationViewModel>();


                string currentApplication = Assembly.GetExecutingAssembly().GetName().Name;

                var output = _cachingService.GetFromDistributedCache("UIApp");

                if(string.IsNullOrEmpty(output))
                {
                    output = currentApplication;
                    await _httpMethods.PostAppName(_httpClientFactory, currentApplication);
                    _cachingService.SetInDistributedCache("UIApp", output);
                }

                var apiList = await _httpMethods.GetApplications(_httpClientFactory, userId);

                foreach (var app in apiList)
                {
                    applications.Add(new ApplicationViewModel()
                    {
                        ApplicationId = app.ApplicationId,
                        ApplicationName = app.ApplicationName,
                        UserId = app.UserId,
                    });
                }

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
