using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Databases
{
    public class BaseManager<TModel> where TModel : Models.BaseModel
    {
        protected Database Database => Database.Instance;

        public virtual Task<IQueryable<TModel>> GetAll()
        {
            return Task.Run(() =>
            {
                return GetIncludes(Database.Set<TModel>());
            });
        }

        public virtual void Save(TModel entity)
        {
            switch (GetState(entity))
            {
                case EntityState.Detached: Add(entity); break;
                case EntityState.Modified: Update(entity); break;
            }
        }

        public virtual TModel Original(TModel entity)
        {
            return Database.Entry(entity).OriginalValues?.ToObject() as TModel;
        }

        public virtual void Reload(TModel entity)
        {
            Database.Entry(entity).Reload();
        }

        public virtual EntityState GetState(TModel entity)
        {
            return Database.Entry(entity).State;
        }

        public virtual void Add(TModel entity)
        {
            entity.UpdatedAt = DateTimeOffset.Now;
            Database.Add(entity);
            Database.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<TModel> entities)
        {
            foreach (var entity in entities)
                entity.UpdatedAt = DateTimeOffset.Now;

            Database.AddRange(entities);
            Database.SaveChanges();
        }

        public virtual void Update(TModel entity)
        {
            entity.UpdatedAt = DateTimeOffset.Now;
            Database.Update(entity);
            Database.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TModel> entities)
        {
            foreach (var entity in entities)
                entity.UpdatedAt = DateTimeOffset.Now;

            Database.UpdateRange(entities);
            Database.SaveChanges();
        }

        public virtual void Remove(TModel entity)
        {
            Database.Remove(entity);
            Database.SaveChanges();
        }

        public virtual void RemoveRange(List<TModel> entities)
        {
            Database.RemoveRange(entities);
            Database.SaveChanges();
        }

        public virtual Task Refresh(TModel entity) => null;

        public virtual Task RefreshRange(List<TModel> entities) => null;

        public virtual IQueryable<TModel> GetIncludes(IQueryable<TModel> entities)
        {
            return entities;
        }

    }
}
