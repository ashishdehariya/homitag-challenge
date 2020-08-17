using HomitagChallenge.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomitagChallenge.DataAccessLayer.Abstract
{
    public interface IRepository<TEntity> where TEntity : BaseModel
    {
        IQueryable<TEntity> GetAll();
        TEntity Get(int id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
        void Save();
    }
}
