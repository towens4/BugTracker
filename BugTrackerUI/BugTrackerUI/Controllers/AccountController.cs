using BugTrackerCore;
using BugTrackerUICore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpMethods _httpMethods;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHttpMethods httpMethods, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpMethods = httpMethods;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        /*
         Create Login and register page
         
         */

        [HttpPost]
        public async Task<IActionResult> Regsiter(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = register.Email, Email = register.Email };
                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    _httpMethods.PostUserId(_httpClientFactory, user.Id.ToString());
                    HttpContext.Session.SetString("Id", user.Id.ToString());
                    return RedirectToAction("Index", "Error");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }

            return View(register);
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            
            if (ModelState.IsValid)
            {
                var userTask = await _userManager.FindByNameAsync(login.Email);
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

                if (result.Succeeded)
                {
                    _httpMethods.PostUserId(_httpClientFactory, userTask.Id.ToString());
                    HttpContext.Session.SetString("Id", userTask.Id.ToString());
                    
                    
                    return RedirectToAction("Index", "Error");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View();
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            _httpMethods.PostUserId(_httpClientFactory, "");
            HttpContext.Session.Remove("Id");
            //assignmentListModel.AssignmentList.ToList().Clear();
            return RedirectToAction("LogIn", "Account");
        }
    }
}
