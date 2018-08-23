using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BD.Models;
using BD.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace BD.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationSignInManager SignInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        private LoginManager UserManager => HttpContext.GetOwinContext().GetUserManager<LoginManager>();
        private readonly IVisitService _visitService;
        private readonly IBaseRepository<Login> _loginRepository;
        private readonly IOrderService _orderService;

        public LoginController(IVisitService visitService, IBaseRepository<Login> loginRepository, IOrderService orderService)
        {
            _visitService = visitService;
            _loginRepository = loginRepository;
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = SignInManager.PasswordSignIn(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            if (result == SignInStatus.Success)
            {
                var login = _loginRepository.Get(x => x.Email == model.Email).FirstOrDefault();
                var lastUrl = _visitService.GetUserLastUrl(login.Id);
                if (string.IsNullOrEmpty(lastUrl))
                {
                    return RedirectToAction("ProductList", "HeadPhone");
                }

                return Redirect(lastUrl);
            }

            ModelState.AddModelError("", @"Login failed.");
            return View(model);
        }

        public ActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SignUp(RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Login { UserName = model.Email, Email = model.Email };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("ProductList", "HeadPhone");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            
            return View(model);
        }

        public ActionResult SetLastUrl(string url)
        {
            if (User.Identity.IsAuthenticated)
            {
                _visitService.SetUserLastUrl(User.Identity.GetUserId(), url);
            }

            return new EmptyResult();
        }

        public ActionResult MyOrders()
        {
            return View(_orderService.GetOrders(User.Identity.GetUserId()));
        }

        public ActionResult LogOff()
        {
            SignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("ProductList", "HeadPhone");
        }
    }
}