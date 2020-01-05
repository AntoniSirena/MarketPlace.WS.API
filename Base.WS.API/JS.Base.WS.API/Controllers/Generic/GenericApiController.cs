using JS.Base.WS.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace JS.Base.WS.API.Controllers.Generic
{
    public class GenericApiController<T> : ApiController where T : class
    {
        private IGenericRepository<T> repository;

        public GenericApiController()
        {
            this.repository = new GenericRepository<T>();
        }


        [HttpGet]
        [Route("GetAll")]
        public virtual IHttpActionResult GetAll()
        {          
            dynamic Entities = repository.GetAll();

            List<object> result = new List<object>();

            foreach (var item in Entities)
            {
                if (item.IsActive)
                {
                    result.Add(item);
                }
            }

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public virtual IHttpActionResult GetById(int id)
        {
            dynamic entity = repository.GetById(id);

            bool isAtive = false;

            if (entity != null)
            {
              isAtive = entity.IsActive;
            }

            if (entity == null || isAtive == false)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        [Route("Create")]
        public virtual IHttpActionResult Create(dynamic entity)
        {
            if (ModelState.IsValid)
            {
                repository.Create(entity);
                repository.Save();
            }
            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public virtual IHttpActionResult Update(dynamic entity)
        {
            if (ModelState.IsValid)
            {                
                repository.Update(entity);
                repository.Save();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public virtual IHttpActionResult Delete(int id)
        {
            var entity = repository.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            repository.Delete(id);
            repository.Save();

            return Ok();
        }


    }

}
