using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Mongo;
using DAL.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DAL.Repositories
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> 
        where TEntity: class, IMongoEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoRepository(IEshopMongoDbContext context, string entityName)
        {
            _collection = context.Database.GetCollection<TEntity>(entityName);
        }

        public void Add(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public void Delete(ObjectId id)
        {
            _collection.DeleteOne(x => x.Id == id);
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> condition, int? skip = null, int? limit = null)
        {
            var filter = new ExpressionFilterDefinition<TEntity>(condition);
            return _collection.Find(filter).Skip(skip).Limit(limit).ToList();
        }

        public void Update(TEntity entity)
        {
            _collection.ReplaceOne(x => x.Id == entity.Id, entity);
        }
    }
}
