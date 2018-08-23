using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BD.Models
{
    [Serializable]
    public class BaseFilter
    {
        public int TradeMarkId { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}