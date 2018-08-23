using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.Models;
using DAL.Entities;
using PagedList;

namespace BD.Services.Interfaces
{
    public interface IHeadPhoneService : IBaseService<HeadPhone, HeadPhoneFilter, HeadPhoneFilterSetting>
    {
    }
}
