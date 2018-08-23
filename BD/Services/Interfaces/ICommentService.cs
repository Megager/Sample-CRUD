using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.Models;
using DAL.Entities;
using DAL.Entities.Mongo;

namespace BD.Services.Interfaces
{
    public interface ICommentService
    {
        void AddComment(CommentModel model, Login login);
        Mark AddMark(MarkModel model, string login);
        IEnumerable<Comment> GetComments(int productId, int? page);
        double GetAvgMark(int productId);
    }
}
