using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.Models;
using DAL.Entities.Interfaces;
using PagedList;

namespace BD.Services.Interfaces
{
    public interface IBaseService<TProduct, TFilter, TFilterSettings>
        where TProduct : class, IProduct
        where TFilter : BaseFilter
        where TFilterSettings: BaseFilterSettings
    {
        PagedList<ProductModel<TProduct>> GetProducts(string userId, int page, TFilter filter);
        void BuyProduct(int id, string userId);
        void SaveFilter(TFilter filter, string userId);
        TFilter GetFilter(string userId);
        ProductModel<TProduct> GetInfo(int id);
        TFilterSettings GetFilterSettings(TFilter filter);
    }
}
