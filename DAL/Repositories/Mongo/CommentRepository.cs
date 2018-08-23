using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Mongo;
using DAL.Repositories.Mongo.Interfaces;

namespace DAL.Repositories.Mongo
{
    public class CommentRepository : MongoRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IEshopMongoDbContext context) : base(context, "Comment")
        {
        }
    }
}
