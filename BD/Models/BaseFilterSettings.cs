using System.Web.Mvc;

namespace BD.Services.Interfaces
{
    public class BaseFilterSettings
    {
        public SelectList TradeMarks { get; set; }
        public int SelectedMinPrice { get; set; }
        public int SelectedMaxPrice { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}