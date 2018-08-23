using System;
using BD.Models;
using BD.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BD.Services
{
    public class VisitService : IVisitService
    {
        private readonly IBaseRepository<View> _viewRepository;

        public VisitService(IBaseRepository<View> viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public void SetUserLastUrl(string userId, string url)
        {
            string key = userId + "lasturl";
            Helper.RedisDatabase.StringSet(Helper.ToByteArray(key), Helper.ToByteArray(url));
        }

        public string GetUserLastUrl(string userId)
        {
            string key = userId + "lasturl";
            return Helper.FromByteArray<string>(Helper.RedisDatabase.StringGet(Helper.ToByteArray(key)));
        }

        public void AddView(int productId)
        {
            _viewRepository.Create(new View
            {
                CreatedDate = DateTime.Now,
                ProductId = productId
            });
        }
    }
}