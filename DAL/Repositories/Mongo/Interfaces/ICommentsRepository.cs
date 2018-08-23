using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Mongo;

namespace DAL.Repositories.Mongo.Interfaces
{
    public interface ICommentsRepository : IMongoRepository<Comments>
    {
    }
}
