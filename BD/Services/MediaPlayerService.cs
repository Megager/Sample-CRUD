using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BD.Models;
using BD.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DAL.Repositories.Mongo.Interfaces;

namespace BD.Services
{
    public class MediaPlayerService : BaseService<MediaPlayer, MediaPlayerFilter, MediaPlayerFilterSettings>, IMediaPlayerService
    {
        private readonly IBaseRepository<MediaPlayer> _mediaPlayerRepository;

        public MediaPlayerService(IBaseRepository<MediaPlayer> mediaPlayerRepository,
                                  IBaseRepository<ProductType> productTypeRepository,
                                  IBaseRepository<Product> productRepository,
                                  IBaseRepository<Order> orderRepository,
                                  IBaseRepository<TradeMark> tradeMarkRepository,
                                  ICommentService commentService) 
                                  : base(productRepository, mediaPlayerRepository, productTypeRepository, "MediaPlayer", orderRepository, tradeMarkRepository, commentService)
        {
            _mediaPlayerRepository = mediaPlayerRepository;
        }
        
        protected override Func<MediaPlayer, bool> ApplyInfoFilter(MediaPlayerFilter filter)
        {
            return mediaPlayer => filter == null || (filter.Memory == 0 || mediaPlayer.Memory > filter.Memory)
                                  && (filter.IsPortable == null || mediaPlayer.IsPortable == filter.IsPortable)
                                  && (string.IsNullOrEmpty(filter.MaximumResolution) || int.Parse(filter.MaximumResolution.Split('x')[0]) <= mediaPlayer.MaxWidthResolution)
                                  && (string.IsNullOrEmpty(filter.MaximumResolution) || int.Parse(filter.MaximumResolution.Split('x')[1]) <= mediaPlayer.MaxHeightResolution);
        }

        public override MediaPlayerFilterSettings GetFilterSettings(MediaPlayerFilter filter)
        {
            int chosenHeight = 0;
            int chosenWidth = 0;

            filter = filter ?? new MediaPlayerFilter();

            if (!string.IsNullOrEmpty(filter.MaximumResolution))
            {
                chosenWidth = int.Parse(filter.MaximumResolution.Split('x')[0]);
                chosenHeight = int.Parse(filter.MaximumResolution.Split('x')[1]);
            }

            var baseFilterSettings = GetBaseFilterSettings(filter);

            var heightResolutions = _mediaPlayerRepository.GetAll()
                                                          .Select(x=>x.MaxHeightResolution)
                                                          .Distinct()
                                                          .OrderByDescending(x => x)
                                                          .Select(x => new SelectListItem
                                                          {
                                                              Value = x.ToString(),
                                                              Text = x.ToString()
                                                          })
                                                          .ToList();

            heightResolutions.Add(new SelectListItem
            {
                Text = "Not chosen",
                Value = "0"
            });

            var widthResolutions = _mediaPlayerRepository.GetAll()
                                                         .Select(x => x.MaxWidthResolution)
                                                         .Distinct()
                                                         .OrderByDescending(x => x)
                                                         .Select(x => new SelectListItem
                                                         {
                                                             Value = x.ToString(),
                                                             Text = x.ToString()
                                                         })
                                                         .ToList();

            widthResolutions.Add(new SelectListItem
            {
                Text = "Not chosen",
                Value = "0"
            });

            var maxMemory = _mediaPlayerRepository.GetAll().Max(x => x.Memory);

            return new MediaPlayerFilterSettings
            {
                MaxPrice = baseFilterSettings.MaxPrice,
                MinPrice = baseFilterSettings.MinPrice,
                SelectedMaxPrice = baseFilterSettings.SelectedMaxPrice,
                SelectedMinPrice = baseFilterSettings.SelectedMinPrice,
                TradeMarks = baseFilterSettings.TradeMarks,
                IsPortable = filter.IsPortable,
                HeightResolutions = new SelectList(heightResolutions, "Value", "Text", chosenHeight.ToString()),
                WidthResolutions = new SelectList(widthResolutions, "Value", "Text", chosenWidth.ToString()),
                MaxMemory = maxMemory,
                Memory = filter.Memory
            };
        }
    }
}