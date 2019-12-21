using JS.Base.WS.API.Controllers.Authorization;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            obj.CreatorUserId = CurrentUser.GetId();
            obj.IsActive = true;

            //Convirtiendo el objeto dinamico a la entidad acutal
            T entity = JsonConvert.DeserializeObject<T>(obj.ToString());

            table.Attach(entity);
            table.Add(entity);
        }

        public virtual void Update(dynamic obj)
        {
            obj.LastModificationTime = DateTime.Now;
            obj.LastModifierUserId = CurrentUser.GetId();
            obj.IsActive = true;
            obj.IsDeleted = false;

            //Convirtiendo el objeto dinamico a la entidad acutal
            T entity = JsonConvert.DeserializeObject<T>(obj.ToString());

            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            //Delete currentUserId in Cache 
            CurrentUser.DeleteId();
        }

        public virtual void Delete(object id)
        {
            dynamic entity = table.Find(id);

            entity.DeletionTime = DateTime.Now;
            entity.DeleterUserId = CurrentUser.GetId();
            entity.IsActive = false;
            entity.IsDeleted = true;

            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            //Delete currentUserId in Cache 
            CurrentUser.DeleteId();
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

    }
}