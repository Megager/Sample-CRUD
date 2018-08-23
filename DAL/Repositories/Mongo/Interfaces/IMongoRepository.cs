using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.Repositories.Mongo.Interfaces
{
    public interface IMongoRepository<TEntity> 
        where TEntity : class 
    {
        void Add(TEntity entity);
        void Delete(ObjectId id);
        IList<TEntity> Find(Expression<Func<TEntity, bool>> condition, int? skip = null, int? limit = null);
        void Update(TEntity entity);
    }
}
