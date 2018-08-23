using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly EshopDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(EshopDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        
        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Create(TEntity item)
        {
            var entity = _dbSet.Add(item);
            _context.SaveChanges();
            return entity;
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
