using JS.Base.WS.API.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace JS.Base.WS.API.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MyDBcontext _context = null;
        private IDbSet<T> table = null;


        public GenericRepository()
        {
           _context = new MyDBcontext();
            table = _context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public virtual T GetById(object id)
        {
            return table.Find(id);
        }

        public virtual void Create(dynamic obj)
        {
            obj.CreationTime = DateTime.Now;
            obj.CreatorUserId = 1;
            obj.IsActive = true;

            table.Add(obj);
        }

        public virtual void Update(dynamic obj)
        {
            obj.LastModificationTime = DateTime.Now;
            obj.LastModifierUserId = 1;
            obj.IsActive = true;
            obj.IsDeleted = false;

            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            dynamic entity = table.Find(id);

            entity.DeletionTime = DateTime.Now;
            entity.DeleterUserId = 1;
            entity.IsActive = false;
            entity.IsDeleted = true;

            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

    }
}