using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BD.Services.Interfaces;

namespace BD.Models
{
    public class LoudspeakerFilterSettings : BaseFilterSettings
    {
        public int MaxVolume { get; set; }
        public int FilterMaxVolume { get; set; }
        public SelectList SoundSystems { get; set; }
        public SelectList Numbers { get; set; }
    }
}