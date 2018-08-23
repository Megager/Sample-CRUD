using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.Models;
using DAL.Entities;

namespace BD.Services.Interfaces
{
    public interface ILoudspeakerService : IBaseService<Loudspeaker, LoudspeakerFilter, LoudspeakerFilterSettings>
    {
    }
}
