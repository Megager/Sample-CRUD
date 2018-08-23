using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BD.Services.Interfaces;

namespace BD.Models
{
    public class MediaPlayerFilterSettings : BaseFilterSettings
    {
        public bool? IsPortable { get; set; }
        public SelectList HeightResolutions { get; set; }
        public SelectList WidthResolutions { get; set; }
        public int Memory { get; set; }
        public int MaxMemory { get; set; }
    }
}