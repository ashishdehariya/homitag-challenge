using HomitagChallenge.DataAccessLayer.Abstract;
using HomitagChallenge.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomitagChallenge.DataAccessLayer.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        private readonly HomitagDbContext _context;
        private DbSet<TEntity> dbSet;
        public Repository(HomitagDbContext context)
        {
            this._context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            var trackingEntity = dbSet.Add(entity);
            return trackingEntity.Entity;
        }

        public TEntity Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            var trackingEntity = dbSet.Remove(entity);
            return trackingEntity.Entity;
        }

        public TEntity Get(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Attach(entity);
            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            var entities = dbSet;
            return entities;
        }

        public TEntity Update(TEntity entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
