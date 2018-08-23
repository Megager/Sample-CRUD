using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BD.Models;
using BD.Services.Interfaces;
using DAL.Entities;

namespace BD.Controllers
{
    public class LoudspeakerController : BaseController<Loudspeaker, LoudspeakerFilter, LoudspeakerFilterSettings, ILoudspeakerService>
    {
        public LoudspeakerController(ILoudspeakerService productService, ICommentService commentService, IVisitService visitService) 
            : base(productService, commentService, visitService)
        {
        }
    }
}