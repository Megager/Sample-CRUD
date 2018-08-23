using System.Collections.Generic;
using System.Web.Mvc;

namespace BD.Services.Interfaces
{
    public class HeadPhoneFilterSetting : BaseFilterSettings
    {
        public int MinFrequencyRange { get; set; }
        public int MaxFrequencyRange { get; set; }
        public int FilterMinFrequencyRange { get; set; }
        public int FilterMaxFrequencyRange { get; set; }
        public SelectList Scopes { get; set; }
        public SelectList Types { get; set; }
    }
}