using JS.Base.WS.API.Base;
using JS.Base.WS.API.Base.IBase;
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

        List<dynamic> RecordsList = new List<dynamic>();

        Response response = new Response();

        public GenericApiController()
        {
            this.repository = new GenericRepository<T>();
        }


        [HttpGet]
        [Route("GetAll")]
        public virtual IHttpActionResult GetAll()
        {
            dynamic Entities = repository.GetAll();

            foreach (var item in Entities)
            {
                if (item.IsActive)
                {
                    RecordsList.Add(item);
                }
            }

            if (RecordsList.Count() == 0)
            {
                return NotFound();
            }

            var result = RecordsList.OrderByDescending(i => i.Id);

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
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Create(entity);
                    repository.Save();

                    response.Message = "Registro creado con exito";
                    return Ok(response);
                }
            }
            catch(Exception)
            {
                response.Code = "021";
                response.Message = "Estimado usuario has ocurrido un error procesando su solicitud";
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        public virtual IHttpActionResult Update(dynamic entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Update(entity);
                    repository.Save();

                    response.Message = "Registro actualizado con exito";
                    return Ok(response);
                }
            }
            catch
            {
                response.Code = "022";
                response.Message = "Estimado usuario has ocurrido un error procesando su solicitud";
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public virtual IHttpActionResult Delete(int id)
        {
            try
            {
                var entity = repository.GetById(id);

                if (entity == null)
                {
                    return NotFound();
                }

                repository.Delete(id);
                repository.Save();

                response.Message = "Registro eliminado con exito";
                return Ok(response);
            }
            catch
            {
                response.Code = "023";
                response.Message = "Estimado usuario has ocurrido un error procesando su solicitud";
            }
            return Ok(response);

        }


        ~GenericApiController()
        {
            this.Dispose();
        }

        private void Dispose()
        {
            RecordsList = null;
        }


    }

}
