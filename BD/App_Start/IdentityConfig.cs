using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using BD.Models;
using DAL;
using DAL.Entities;

namespace BD
{
    public class LoginManager : UserManager<Login>
    {
        private LoginManager(IUserStore<Login> store)
            : base(store)
        {
        }

        public static LoginManager Create(IdentityFactoryOptions<LoginManager> options, IOwinContext context) 
        {
            var manager = new LoginManager(new UserStore<Login>(context.Get<EshopDbContext>()));
            manager.UserValidator = new UserValidator<Login>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<Login>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
    
    public class ApplicationSignInManager : SignInManager<Login, string>
    {
        private ApplicationSignInManager(LoginManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Login user)
        {
            return ((LoginManager)UserManager).CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<LoginManager>(), context.Authentication);
        }
    }
}
