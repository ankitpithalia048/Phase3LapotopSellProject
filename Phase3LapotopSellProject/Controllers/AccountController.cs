using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Phase3LapotopSellProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _securityManager;
        private readonly SignInManager<ApplicationUser> _loginManager;
        //Constructor
        public AccountController(UserManager<ApplicationUser> secMgr, SignInManager<ApplicationUser> loginManager)
        {
            _securityManager = secMgr;
            _loginManager = loginManager;

        }
        public string AccessDenied(string url)
        {
            return "Access Denied";
        }
        //Register Get
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        //Register Post

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
            using (var context = new AppDbContext())
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Name,
                        Email = model.Email,
                        Address = model.Address,
                        PhoneNumber = model.Phone
                    };
                    var result = await _securityManager.CreateAsync(user, model.Password);

                    /*if (!result.Succeeded)
                    {
                            
                        ViewBag.RegisterError = result.Errors;
                        return View();
                    }*/

                    if (result.Succeeded)
                    {
                        await _securityManager.AddToRoleAsync(user, "User");
                        
                        await _loginManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction(nameof(HomeController.Index), "Home");

                    }
                    foreach (var error in result.Errors)
                    {
                        if (error.Code.Contains("Password"))
                        {
                            ViewData["Password"] = ViewData["Password"] + error.Description +"\n";
                        }
                        if (error.Code.Contains("UserName"))
                        {
                            ViewData["UserName"] = ViewData["UserName"] + error.Description + "\n";
                        }
                        else
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                    }


                }
            }

            return View(model);
        }

        //Login Get
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Login");
        }

        //Login Post
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _loginManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                

                    return RedirectToReturnUrl(returnUrl);
                }
                if (!result.Succeeded)
                {
                    ViewBag.Failed = "UserName or Password is not correct.";
                    return View();
                }


            }


            return View(model);
        }

        //LogOut

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _loginManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        //ReturntoRedirect
        private IActionResult RedirectToReturnUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
