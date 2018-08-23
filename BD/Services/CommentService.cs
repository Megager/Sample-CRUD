using System;
using System.Collections.Generic;
using System.Linq;
using BD.Models;
using BD.Services.Interfaces;
using DAL.Entities;
using DAL.Entities.Mongo;
using DAL.Repositories.Mongo.Interfaces;
using MongoDB.Bson;

namespace BD.Services
{
    public class CommentService : ICommentService
    {
        private const int CommentPageSize = 8;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IMarkRepository _markRepository;

        public CommentService(ICommentsRepository commentsRepository,
            IMarkRepository markRepository)
        {
            _markRepository = markRepository;
            _commentsRepository = commentsRepository;
        }

        public void AddComment(CommentModel model, Login login)
        {
            var comments = _commentsRepository.Find(x => x.ProductId == model.ProductId).FirstOrDefault();
            if (comments == null)
            {
                comments = new Comments
                {
                    Id = ObjectId.GenerateNewId(),
                    CommentList = new List<Comment>(),
                    ProductId = model.ProductId,
                    MarkList = new List<Mark>()
                };

                _commentsRepository.Add(comments);
            }

            comments.CommentList.Add(new Comment
            {
                CreatedDate = DateTime.Now,
                Id = ObjectId.GenerateNewId(),
                Text = model.Description,
                UserId = login.Id,
                Name = login.UserName
            });

            _commentsRepository.Update(comments);
        }

        public Mark AddMark(MarkModel model, string login)
        {
            var comments = _commentsRepository.Find(x => x.ProductId == model.ProductId).FirstOrDefault();
            if (comments == null)
            {
                comments = new Comments
                {
                    Id = ObjectId.GenerateNewId(),
                    CommentList = new List<Comment>(),
                    ProductId = model.ProductId,
                    MarkList = new List<Mark>()
                };

                _commentsRepository.Add(comments);
            }

            if (comments.MarkList == null)
            {
                comments.MarkList = new List<Mark>();
            }

            var oldMark = comments.MarkList.FirstOrDefault(x => x.UserId == login);
            if (oldMark != null)
            {
                _markRepository.Delete(oldMark.Id);
            }

            var newMark = new Mark
            {
                Id = ObjectId.GenerateNewId(DateTime.Now),
                ProductMark = model.Mark,
                UserId = login
            };

            comments.MarkList.Add(newMark);
            _commentsRepository.Update(comments);
            return newMark;
        }
        
        public IEnumerable<Comment> GetComments(int productId, int? page = null)
        {
            var comments = _commentsRepository.Find(x => x.ProductId == productId).FirstOrDefault();
            if (comments == null)
            {
                return new List<Comment>();
            }
            if (page == null)
            {
                page = 0;
            }

            return comments.CommentList.Skip(CommentPageSize * ((int)page - 1)).Take(CommentPageSize);
        }

        public double GetAvgMark(int productId)
        {
            var comments = _commentsRepository.Find(x => x.ProductId == productId).FirstOrDefault();
            return comments?.MarkList?.Average(x => x.ProductMark) ?? 0;
        }
    }
}