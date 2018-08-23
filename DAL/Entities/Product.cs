using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Product : Entity
    {
        public int Price { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public int InfoId { get; set; }

        [ForeignKey("TradeMark")]
        public int TradeMarkId { get; set; }
        public virtual TradeMark TradeMark { get; set; }

        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        
        public virtual ICollection<Image> Images { get; set; }
    }
}
