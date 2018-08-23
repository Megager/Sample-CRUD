using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Mongo;
using DAL.Repositories.Mongo.Interfaces;

namespace DAL.Repositories.Mongo
{
    public class MarkRepository : MongoRepository<Mark>, IMarkRepository
    {
        public MarkRepository(IEshopMongoDbContext context) : base(context, "Mark")
        {
        }
    }
}
