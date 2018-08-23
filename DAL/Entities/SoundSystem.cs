using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class SoundSystem : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Loudspeaker> Loudspeakers { get; set; }
    }
}
