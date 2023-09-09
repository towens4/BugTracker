﻿using BugTrackerUI.Helper;
using BugTrackerUI.Models;
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
        public async Task<ActionResult> AddApplication([FromBody] Models.ViewModels.ApplicationNameInputModel applicationName)
        {
            try
            {
                var appiName = Assembly.GetExecutingAssembly().GetName().Name;
                string appName = applicationName.ApplicationName;
                IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
                ApplicationViewModel application = new ApplicationViewModel() { ApplicationId = new Guid(), ApplicationName = appName, UserId = user.Id };
                ApiHandler.AddApplication(_httpClient, application);
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
    }
}