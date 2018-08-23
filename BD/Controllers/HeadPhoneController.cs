using System;
using System.Web;
using System.Web.Mvc;
using BD.Models;
using BD.Services.Interfaces;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BD.Controllers
{
    public class HeadPhoneController : BaseController<HeadPhone, HeadPhoneFilter, HeadPhoneFilterSetting, IHeadPhoneService>
    {
        public HeadPhoneController(IHeadPhoneService productService, ICommentService commentService, IVisitService visitService) 
            : base(productService, commentService, visitService)
        {
        }
    }
}