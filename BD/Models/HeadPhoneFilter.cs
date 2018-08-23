using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD.Models
{
    [Serializable]
    public class HeadPhoneFilter : BaseFilter
    {
        public int MinFrequencyRange { get; set; }
        public int MaxFrequencyRange { get; set; }
        public int ScopeId { get; set; }
        public int TypeId { get; set; }
    }
}