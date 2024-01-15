
using BugTrackerAPICall.Interfaces;
using BugTrackerUICore.Helper;
using BugTrackerUICore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace BugTrackerUI.ViewComponents
{
    public class ErrorListViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpMethods _httpMethods;
        public ErrorListViewComponent(IHttpClientFactory httpClient, UserManager<IdentityUser> userManager, IHttpMethods httpMethods)
        {
            _httpClient = httpClient;
            _userManager = userManager;
            _httpMethods = httpMethods;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(Guid applicationId)
        {

            string applicationName = Assembly.GetExecutingAssembly().GetName().Name;
            List<ErrorViewModel> errorList = new List<ErrorViewModel>();
            try
            {
                //IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
                var userId = HttpContext.Session.GetString("Id");
                //UserAccess.PostCurrentUser();
                //IdHolderModel idHolder = IdHolderFactory.CreateIdHolder(userId, applicationId);


                var errorApiList = await _httpMethods.GetErrors(_httpClient, applicationId);

                if (errorApiList == null || !errorApiList.Any())
                    return View(errorList);

                errorList = ErrorFactory.CreateErrorList(errorApiList);


                return View(errorList);
            }
            catch (Exception ex)
            {
                await _httpMethods.AddError(_httpClient, ex, applicationName, BugTrackerAPICall.Helper.CallerMethod.GetCallerMethodName());
                return View(errorList);
            }
        }
    }
}
