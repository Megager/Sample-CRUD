using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DAL.Entities.Mongo
{
    public class Comments : IMongoEntity
    {
        public ObjectId Id { get; set; }
        public int ProductId { get; set; }
        public ICollection<Comment> CommentList { get; set; }
        public ICollection<Mark> MarkList { get; set; }
   }
}
