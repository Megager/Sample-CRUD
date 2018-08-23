using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BD.Models;
using BD.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using PagedList;
using BD.Services;
using DAL.Repositories.Mongo.Interfaces;

namespace BD.Services
{
    public class HeadPhoneService : BaseService<HeadPhone, HeadPhoneFilter, HeadPhoneFilterSetting>, IHeadPhoneService
    {
        private readonly IBaseRepository<DAL.Entities.Type> _typeRepository;
        private readonly IBaseRepository<Scope> _scopeRepository;
        private readonly IBaseRepository<HeadPhone> _headPhoneRepository;

        public HeadPhoneService(IBaseRepository<HeadPhone> headPhoneRepository,
                                IBaseRepository<ProductType> productTypeRepository,
                                IBaseRepository<Product> productRepository,
                                IBaseRepository<Order> orderRepository,
                                IBaseRepository<DAL.Entities.Type> typeRepository,
                                IBaseRepository<Scope> scopeRepository,
                                IBaseRepository<TradeMark> tradeMarkRepository,
                                ICommentService commentService) 
                                : base(productRepository, headPhoneRepository, productTypeRepository, "HeadPhone", orderRepository, tradeMarkRepository, commentService)
        {
            _headPhoneRepository = headPhoneRepository;
            _typeRepository = typeRepository;
            _scopeRepository = scopeRepository;
        }

        protected override Func<HeadPhone, bool> ApplyInfoFilter(HeadPhoneFilter filter)
        {
            return headPhone => filter == null || (filter.MaxFrequencyRange == _headPhoneRepository.GetAll().Max(x => x.MaxFrequencyRange) || headPhone.MaxFrequencyRange <= filter.MaxFrequencyRange) 
                                && (filter.MinFrequencyRange == _headPhoneRepository.GetAll().Min(x => x.MinFrequencyRange) || headPhone.MinFrequencyRange >= filter.MinFrequencyRange)
                                && (filter.ScopeId == 0 || filter.ScopeId == headPhone.ScopeId) 
                                && (filter.TypeId == 0 || filter.TypeId == headPhone.TypeId);
        }


        public override HeadPhoneFilterSetting GetFilterSettings(HeadPhoneFilter filter)
        {
            filter = filter ?? new HeadPhoneFilter();
            var types = _typeRepository.GetAll().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            types.Add(new SelectListItem
            {
                Text = "Not chosen",
                Value = "0"
            });
            
            var scopes = _scopeRepository.GetAll().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            scopes.Add(new SelectListItem
            {
                Text = "Not chosen",
                Value = "0"
            });

            var maxFrequencyRange = _headPhoneRepository.GetAll().Max(x => x.MaxFrequencyRange);
            var minFrequencyRange = _headPhoneRepository.GetAll().Min(x => x.MinFrequencyRange);
            var baseFilterSettings = GetBaseFilterSettings(filter);
            
            return new HeadPhoneFilterSetting
            {
                MaxPrice = baseFilterSettings.MaxPrice,
                MinPrice = baseFilterSettings.MinPrice,
                SelectedMaxPrice = baseFilterSettings.SelectedMaxPrice,
                SelectedMinPrice = baseFilterSettings.SelectedMinPrice,
                TradeMarks = baseFilterSettings.TradeMarks,
                MaxFrequencyRange = maxFrequencyRange,
                MinFrequencyRange = minFrequencyRange,
                FilterMaxFrequencyRange = filter.MaxFrequencyRange != 0 ? filter.MaxFrequencyRange : maxFrequencyRange,
                FilterMinFrequencyRange = filter.MinFrequencyRange != 0 ? filter.MinFrequencyRange : minFrequencyRange,
                Scopes = new SelectList(scopes, "Value", "Text", filter.ScopeId.ToString()),
                Types = new SelectList(types, "Value", "Text", filter.TypeId.ToString())
            };
        }
    }
}