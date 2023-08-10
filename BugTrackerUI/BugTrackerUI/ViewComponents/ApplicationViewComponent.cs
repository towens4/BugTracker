using BugTrackerUI.Models;
using BugTrackerUI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BugTrackerUI.Helper;

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
            IEnumerable<ApplicationViewModel> applications = null;
            IdentityUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var userId = currentUser.Id;

            //ApiHandler apiHandler = new ApiHandler();

            //applications = await apiHandler.LoadApplications(userId);

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7240/api/error/{userId}");

            var client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                applications = await response.Content.ReadFromJsonAsync<List<ApplicationViewModel>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

            if(applications == null)
                return View(new List<ApplicationViewModel>());

            return View(applications);
        }
    }
}
