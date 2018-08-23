using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD.Models
{
    [Serializable]
    public class LoudspeakerFilter : BaseFilter
    {
        public int Number { get; set; }
        public int MaxVolume { get; set; }
        public int SoundSystemId { get; set; }
    }
}