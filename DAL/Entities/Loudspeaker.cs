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
    public class Loudspeaker : Entity, IProduct
    {
        public int Number { get; set; }
        public int MaxVolume { get; set; }

        [ForeignKey("SoundSystem")]
        public int SoundSystemId { get; set; }
        public virtual SoundSystem SoundSystem { get; set; }
    }
}
