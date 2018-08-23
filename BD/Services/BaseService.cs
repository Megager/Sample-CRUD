using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BD.Models;
using BD.Services.Interfaces;
using Couchbase;
using DAL.Entities;
using DAL.Entities.Interfaces;
using DAL.Repositories.Interfaces;
using DAL.Repositories.Mongo.Interfaces;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using PagedList;

namespace BD.Services
{
    public abstract class BaseService<TProduct, TFilter, TFilterSettings> : IBaseService<TProduct, TFilter, TFilterSettings>
        where TProduct : class, IProduct
        where TFilter : BaseFilter, new()
        where TFilterSettings : BaseFilterSettings, new ()
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<TProduct> _tproductRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly ProductType _productType;
        private readonly IBaseRepository<TradeMark> _tradeMarkRepository;
        private readonly ICommentService _commentService;
        private const int PageSize = 8;

        protected BaseService(IBaseRepository<Product> productRepository,
                              IBaseRepository<TProduct> tproductRepository,
                              IBaseRepository<ProductType> productTypeRepository,
                              string productTypeName,
                              IBaseRepository<Order> orderRepository,
                              IBaseRepository<TradeMark> tradeMarkRepository,
                              ICommentService commentService)
        {
            _productRepository = productRepository;
            _tproductRepository = tproductRepository;
            _productType = productTypeRepository.Get(pt => pt.Name == productTypeName).FirstOrDefault();
            _orderRepository = orderRepository;
            _tradeMarkRepository = tradeMarkRepository;
            _commentService = commentService;
        }

        public PagedList<ProductModel<TProduct>> GetProducts(string userId, int page, TFilter filter)
        {
            var products = _productRepository.Get(ApplyBaseFilter(filter));
            var tproducts = _tproductRepository.Get(ApplyInfoFilter(filter));
            var tproductModels = products.Join(tproducts,
                                               product => product.InfoId,
                                               tproduct => tproduct.Id,
                                               (product, tproduct) => new ProductModel<TProduct>
                                               {
                                                   Product = product,
                                                   ProductInfo = tproduct,
                                                   Mark = _commentService.GetAvgMark(product.Id)
                                               });

            return new PagedList<ProductModel<TProduct>>(tproductModels, page, PageSize);
        }

        protected abstract Func<TProduct, bool> ApplyInfoFilter(TFilter filter);

        public abstract TFilterSettings GetFilterSettings(TFilter filter);

        private Func<Product, bool> ApplyBaseFilter(BaseFilter baseFilter)
        {
            return product => product.ProductTypeId == _productType.Id 
                              && (baseFilter == null || product.Count != 0
                              && (baseFilter.MinPrice == 0 || product.Price >= baseFilter.MinPrice)
                              && (baseFilter.MaxPrice == 0 || product.Price <= baseFilter.MaxPrice)
                              && (baseFilter.TradeMarkId == 0 || baseFilter.TradeMarkId == product.TradeMarkId));
        }

        public void BuyProduct(int id, string userId)
        {
            var product = _productRepository.Get(p => p.InfoId == id && p.ProductTypeId == _productType.Id && p.Count > 0).FirstOrDefault();
            if (product == null)
            {
                return;
            }

            _orderRepository.Create(new Order
            {
                Price = product.Price,
                IsClosed = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                LoginId = userId,
                ProductId = product.Id
            });

            product.Count--;
            _productRepository.Update(product);
        }

        public void SaveFilter(TFilter filter, string userId)
        {
            var key = Helper.ToByteArray(userId + _productType.Name);
            Helper.RedisDatabase.StringSet(key, Helper.ToByteArray(filter));
        }

        public TFilter GetFilter(string userId)
        {
            string key = userId + _productType.Name;
            return Helper.FromByteArray<TFilter>(Helper.RedisDatabase.StringGet(Helper.ToByteArray(key)));
        }

        public ProductModel<TProduct> GetInfo(int id)
        {
            var product = _productRepository.Get(x => x.ProductType.Id == _productType.Id && x.InfoId == id).FirstOrDefault();
            var tproduct = _tproductRepository.FindById(id);
            return new ProductModel<TProduct>
            {
                Product = product,
                ProductInfo = tproduct,
                Mark = _commentService.GetAvgMark(product.Id),
                Comments = _commentService.GetComments(product.Id, null)
            };
        }

        protected BaseFilterSettings GetBaseFilterSettings(BaseFilter filter)
        {
            var tradeMarks = _tradeMarkRepository.GetAll().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString(),
                Selected = t.Id == filter.TradeMarkId
            }).ToList();

            tradeMarks.Add(new SelectListItem
            {
                Text = "Not chosen",
                Value = "0",
                Selected = filter.TradeMarkId == 0
            });

            var products = _productRepository.Get(x => x.ProductType.Id == _productType.Id);
            var tproducts = _tproductRepository.GetAll();
            var tproductModels = products.Join(tproducts,
                product => product.InfoId,
                tproduct => tproduct.Id,
                (product, tproduct) => new ProductModel<TProduct>
                {
                    Product = product,
                    ProductInfo = tproduct
                });

            var maxPrice = tproducts.Any() ? tproductModels.Max(x => x.Product.Price) : 0;
            var minPrice = tproducts.Any() ? tproductModels.Min(x => x.Product.Price) : 0;

            return new BaseFilterSettings
            {
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                SelectedMaxPrice = filter.MaxPrice != 0 ? filter.MaxPrice : maxPrice,
                SelectedMinPrice = filter.MinPrice != 0 ? filter.MinPrice : minPrice,
                TradeMarks = new SelectList(tradeMarks, "Value", "Text", filter.TradeMarkId.ToString())
            };
        }
    }
}