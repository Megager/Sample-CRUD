using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.Services.Interfaces
{
    public interface IVisitService
    {
        void SetUserLastUrl(string userId, string url);
        string GetUserLastUrl(string userId);
        void AddView(int productId);
    }
}
