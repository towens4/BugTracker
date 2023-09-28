using BugTrackerUICore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                    HttpContext.Session.SetString("Id", user.Id.ToString());
                    return RedirectToAction("Index", "Assignment");
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
            ModelState.Remove("AssignmentList");
            if (ModelState.IsValid)
            {
                var userTask = await _userManager.FindByNameAsync(login.Email);
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("Id", userTask.Id.ToString());
                    return RedirectToAction("Index", "Assignment");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View();
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            //assignmentListModel.AssignmentList.ToList().Clear();
            return RedirectToAction("LogIn", "Account");
        }
    }
}
