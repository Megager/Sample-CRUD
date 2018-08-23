using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BD.Models;
using BD.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BD.Services
{
    public class LoudspeakerService : BaseService<Loudspeaker, LoudspeakerFilter, LoudspeakerFilterSettings>, ILoudspeakerService
    {
        private readonly IBaseRepository<Loudspeaker> _loudspeakerRepository;
        private readonly IBaseRepository<SoundSystem> _soundSystemRepository;

        public LoudspeakerService(IBaseRepository<Loudspeaker> loudspeakerRepository,
                                  IBaseRepository<ProductType> productTypeRepository,
                                  IBaseRepository<Product> productRepository, 
                                  IBaseRepository<Order> orderRepository,
                                  IBaseRepository<TradeMark> tradeMarkRepository,
                                  ICommentService commentService,
                                  IBaseRepository<SoundSystem> soundSystemRepository) 
                                  : base(productRepository, loudspeakerRepository, productTypeRepository, "Loudspeaker", orderRepository, tradeMarkRepository, commentService)
        {
            _loudspeakerRepository = loudspeakerRepository;
            _soundSystemRepository = soundSystemRepository;
        }

        protected override Func<Loudspeaker, bool> ApplyInfoFilter(LoudspeakerFilter filter)
        {
            return loudspeaker => filter == null || (filter.Number == 0 || loudspeaker.Number == filter.Number)
                                  && (filter.SoundSystemId == 0 || filter.SoundSystemId == loudspeaker.SoundSystemId)
                                  && (filter.MaxVolume == 0 || filter.MaxVolume <= loudspeaker.MaxVolume);
        }

        public override LoudspeakerFilterSettings GetFilterSettings(LoudspeakerFilter filter)
        {
            filter = filter ?? new LoudspeakerFilter();
            var baseFilterSettings = GetBaseFilterSettings(filter);
            var maxVolume = _loudspeakerRepository.GetAll().Max(x => x.MaxVolume);
            var soundSystems = _soundSystemRepository.GetAll().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            soundSystems.Add(new SelectListItem
            {
                Text = "Not chosen",
                Value = "0"
            });

            var numbers = _loudspeakerRepository.GetAll()
                                                .Select(x => x.Number)
                                                .Distinct()
                                                .Select(n => new SelectListItem
                                                {
                                                    Text = n.ToString(),
                                                    Value = n.ToString()
                                                })
                                                .ToList();

            numbers.Add(new SelectListItem
            {
                Text = "Not chosen",
                Value = "0"
            });

            return new LoudspeakerFilterSettings
            {
                MaxPrice = baseFilterSettings.MaxPrice,
                MinPrice = baseFilterSettings.MinPrice,
                SelectedMaxPrice = baseFilterSettings.SelectedMaxPrice,
                SelectedMinPrice = baseFilterSettings.SelectedMinPrice,
                TradeMarks = baseFilterSettings.TradeMarks,
                MaxVolume = maxVolume,
                FilterMaxVolume = filter.MaxVolume,
                SoundSystems = new SelectList(soundSystems, "Value", "Text", filter.SoundSystemId.ToString()),
                Numbers = new SelectList(numbers, "Value", "Text", filter.Number.ToString())
            };
        }
    }
}