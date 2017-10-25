using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreTour.Models;
using Microsoft.AspNetCore.Identity;
using CoreTour.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreTour.Controllers.Web
{
    public class AuthController : Controller
    {
        private SignInManager<CoreTourUser> _signInManager;

        public AuthController(SignInManager<CoreTourUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Trips", "App");
                }
            }

            ModelState.AddModelError("", "Login Failed");
            return View();
        }
    }
}
