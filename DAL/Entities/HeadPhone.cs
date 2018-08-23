using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Interfaces;

namespace DAL.Entities
{
    public class HeadPhone : Entity, IProduct
    {
        public int MinFrequencyRange { get; set; }
        public int MaxFrequencyRange { get; set; }

        [ForeignKey("Scope")]
        public int ScopeId { get; set; }
        public virtual Scope Scope { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
    }
}
