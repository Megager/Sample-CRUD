using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BD.Models;
using BD.Services;
using BD.Services.Interfaces;
using DAL.Entities.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BD.Controllers
{
    public class BaseController<TProduct, TFilter, TFilterSettings, TProductService> : Controller
        where TProduct : class, IProduct
        where TFilter : BaseFilter, new()
        where TFilterSettings : BaseFilterSettings, new()
        where TProductService : IBaseService<TProduct, TFilter, TFilterSettings>
    {
        private readonly TProductService _productService;
        private readonly ICommentService _commentService;
        private readonly IVisitService _visitService;
        private LoginManager UserManager => HttpContext.GetOwinContext().GetUserManager<LoginManager>();

        public BaseController(TProductService productService, ICommentService commentService, IVisitService visitService)
        {
            _productService = productService;
            _commentService = commentService;
            _visitService = visitService;
        }

        public ActionResult ProductList(int page = 1)
        {
            var id = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : (string)Session["tempId"];
            var filter = _productService.GetFilter(id);
            ViewBag.filter = _productService.GetFilterSettings(filter);
            return View(_productService.GetProducts(User.Identity.GetUserId(), page, filter));
        }

        public ActionResult Info(int id, int productId)
        {
            _visitService.AddView(productId);
            return View(_productService.GetInfo(id));
        }

        public ActionResult AddComment(CommentModel model)
        {
            _commentService.AddComment(model, UserManager.FindById(User.Identity.GetUserId()));
            return new EmptyResult();
        }

        public ActionResult AddMark(MarkModel model)
        {
            _commentService.AddMark(model, User.Identity.GetUserId());
            return new EmptyResult();
        }

        public ActionResult BuyProduct(int id)
        {
            _productService.BuyProduct(id, User.Identity.GetUserId());
            return RedirectToAction("ProductList");
        }
        
        public ActionResult SaveFilter(TFilter filter)
        {
            string id;
            if (User.Identity.IsAuthenticated)
            {
                id = User.Identity.GetUserId();
            }
            else
            {
                id = Guid.NewGuid().ToString();
                Session["tempId"] = id;
            }

            _productService.SaveFilter(filter, id);
            return new EmptyResult();
        }
    }
}