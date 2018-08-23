using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Interfaces;

namespace DAL.Entities
{
    public class MediaPlayer : Entity, IProduct
    {
        public int MaxWidthResolution { get; set; }
        public int MaxHeightResolution { get; set; }
        public int Memory { get; set; }
        public bool IsPortable { get; set; }
    }
}
