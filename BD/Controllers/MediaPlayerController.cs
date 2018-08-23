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
    public class MediaPlayerController : BaseController<MediaPlayer, MediaPlayerFilter, MediaPlayerFilterSettings, IMediaPlayerService>
    {
        public MediaPlayerController(IMediaPlayerService productService, ICommentService commentService, IVisitService visitService) 
            : base(productService, commentService, visitService)
        {
        }
    }
}