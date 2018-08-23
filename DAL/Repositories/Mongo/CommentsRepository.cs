using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Mongo;
using DAL.Repositories.Mongo.Interfaces;

namespace DAL.Repositories.Mongo
{
    public class CommentsRepository : MongoRepository<Comments>, ICommentsRepository
    {
        public CommentsRepository(IEshopMongoDbContext context) : base(context, "Comments")
        {
        }
    }
}
