using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD.Models
{
    [Serializable]
    public class MediaPlayerFilter : BaseFilter
    {
        public string MaximumResolution { get; set; }
        public int Memory { get; set; }
        public bool? IsPortable { get; set; }
    }
}