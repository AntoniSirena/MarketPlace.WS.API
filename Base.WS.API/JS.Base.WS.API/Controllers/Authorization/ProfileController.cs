using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Common;
using JS.Base.WS.API.DTO.Request;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Models.PersonProfile;
using JS.Base.WS.API.Services;
using JS.Base.WS.API.Services.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using static JS.Base.WS.API.Global.Constants;

namespace JS.Base.WS.API.Controllers.Authorization
{
    [RoutePrefix("api/profile")]
    [Authorize]
    public class ProfileController : ApiController
    {
        private ProfileService ProfileService;
        private MyDBcontext db;
        private long currentUserId = CurrentUser.GetId();

        public ProfileController()
        {
            ProfileService = new ProfileService();
            db = new MyDBcontext();
        }


        [HttpGet]
        [Route("GetGenders")]
        public List<GenderDto> GetGenders()
        {
            List<GenderDto> genders = db.Genders.Where(x => x.IsActive == true).Select(y => new GenderDto
            {
                Id = y.Id,
                Description = y.Description,
                ShortName = y.ShortName
            }).ToList();

            return genders;
        }

        [HttpGet]
        [Route("GetLocatorsTypes")]
        public IHttpActionResult GetLocatorsTypes()
        {
            var result = db.LocatorTypes.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description,
            }).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetInfoCurrentUser")]
        public IHttpActionResult GetInfoCurrentUser()
        {
            var result = db.Users.Where(x => x.Id == currentUserId).Select(x => new
            {
                UserName = x.UserName,
                Password = x.Password,
                Name = !string.IsNullOrEmpty(x.Name) ? x.Name : string.Empty,
                SurName = !string.IsNullOrEmpty(x.Surname) ? x.Surname : string.Empty,
                EmailAddress = !string.IsNullOrEmpty(x.EmailAddress) ? x.EmailAddress : string.Empty,
                PhoneNumber = !string.IsNullOrEmpty(x.PhoneNumber) ? x.PhoneNumber : string.Empty,
                Image = x.Image
            }).FirstOrDefault();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetInfoCurrentPerson")]
        public IHttpActionResult GetInfoCurrentPerson()
        {
            var currentUser = db.Users.Where(x => x.Id == currentUserId).FirstOrDefault();

            if (currentUser?.PersonId > 0)
            {
                var result = db.People.Where(x => x.Id == currentUser.PersonId).Select(x => new
                {
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    SurName = x.Surname,
                    SecondSurname = x.secondSurname,
                    BirthDate = x.BirthDate,
                    FullName = x.FullName,
                    GenderId = x.GenderId,
                    DocumentTypeId = x.DocumentTypeId,
                    DocumentNumber = x.DocumentNumber,
                    DocumentDescription = x.DocumentType.Description,
                }).FirstOrDefault();

                return Ok(result);
            }
            else
            {
                var result = new { };
                return Ok(result = null);
            }
        }

        [HttpGet]
        [Route("GetInfoCurrentLocators")]
        public IHttpActionResult GetInfoCurrentLocators()
        {
            var currentUser = db.Users.Where(x => x.Id == currentUserId).FirstOrDefault();

            if (currentUser?.PersonId > 0)
            {
                var result = db.Locators.Where(x => x.PersonId == currentUser.PersonId && x.IsActive == true).Select(x => new
                {
                    Id = x.Id,
                    LocatorTypeId = x.LocatorTypeId,
                    LocatorTypeDescription = x.LocatorType.Description,
                    Description = x.Description,
                    IsMain = x.IsMain
                }).ToList()
                .OrderByDescending(x => x.Id);

                return Ok(result);
            }
            else
            {
                var result = new { };
                return Ok(result = null);
            }
        }

        [HttpPut]
        [Route("UpdateInfoCurrentUser")]
        public IHttpActionResult UpdateInfoCurrentUser(InfoCurrentUser request)
        {
            Response response = new Response();

            try
            {
                var currentUser = db.Users.Where(x => x.Id == currentUserId).FirstOrDefault();

                if (currentUser.UserName != request.UserName)
                {
                    long id = db.Users.Where(x => x.UserName == request.UserName).Select(x => x.Id).FirstOrDefault();

                    if (id > 0)
                    {
                        response.Code = "400";
                        response.Message = string.Concat("El nombre de usuario ", request.UserName, " no esta disponible, favor eliga otro");

                        return Ok(response);
                    }
                }

                if (request.Password != currentUser.Password)
                {
                    if(request.Password.Length < 8)
                    {
                        response.Code = "400";
                        response.Message = "La contraseña debes tener minimo 8 caracteres";

                        return Ok(response);
                    }

                    request.Password = Utilities.Security.Encrypt_OneWay(request.Password);
                }


                //Clear PhoneNumber
                Regex re = new Regex("[;\\\\/:*?\"<>|&' ._-]");
                request.PhoneNumber = re.Replace(request.PhoneNumber, "");

                currentUser.UserName = request.UserName;
                currentUser.Password = request.Password;
                currentUser.Name = request.Name;
                currentUser.Surname = request.SurName;
                currentUser.EmailAddress = request.EmailAddress;
                currentUser.PhoneNumber = request.PhoneNumber;
                currentUser.LastModificationTime = DateTime.Now;
                currentUser.LastModifierUserId = currentUserId;
                db.SaveChanges();

                response.Message = "Registro actualizado con exito";
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            response.Code = "008";
            response.Message = "Petición no procesada";
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateInfoCurrentPerson")]
        public IHttpActionResult UpdateInfoCurrentPerson(InfoCurrentPerson request)
        {
            Response response = new Response();

            try
            {
                var currentUser = db.Users.Where(x => x.Id == currentUserId).FirstOrDefault();

                if (string.IsNullOrEmpty(request.DocumentNumber))
                {
                    request.DocumentNumber = "00000000000000";
                }

                if (request.DocumentTypeId < 1)
                {
                    request.DocumentTypeId = db.DocumentTypes.Where(x => x.ShortName == Global.Constants.DocumentTypes.Cédula).Select(y => y.Id).FirstOrDefault();
                }

                if (currentUser?.PersonId > 0)
                {
                    var currentPerson = db.People.Where(x => x.Id == currentUser.PersonId).FirstOrDefault();

                    currentPerson.FirstName = request.FirstName;
                    currentPerson.secondSurname = request.SecondName;
                    currentPerson.Surname = request.SurName;
                    currentPerson.secondSurname = request.SecondSurname;
                    currentPerson.FullName = request.FirstName + " " + request.SecondName + " " + request.SurName + " " + request.SecondSurname;
                    currentPerson.BirthDate = request.BirthDate;
                    currentPerson.GenderId = request.GenderId;
                    currentPerson.DocumentTypeId = request.DocumentTypeId;
                    currentPerson.DocumentNumber = request.DocumentNumber;
                    currentPerson.LastModificationTime = DateTime.Now;
                    currentPerson.LastModifierUserId = currentUserId;

                    db.SaveChanges();

                    response.Message = "Registro actualizado con exito";
                    return Ok(response);
                }
                else
                {
                    Person person = new Person();

                    if (string.IsNullOrEmpty(request.DocumentNumber))
                    {
                        request.DocumentNumber = "00000000000000";
                    }

                    if (request.DocumentTypeId < 1)
                    {
                        request.DocumentTypeId = db.DocumentTypes.Where(x => x.ShortName == Global.Constants.DocumentTypes.Cédula).Select(y => y.Id).FirstOrDefault();
                    }

                    person.FirstName = request.FirstName;
                    person.SecondName = request.SecondName;
                    person.Surname = request.SurName;
                    person.secondSurname = request.SecondSurname;
                    person.FullName = request.FirstName + " " + request.SecondName + " " + request.SurName + " " + request.SecondSurname;
                    person.BirthDate = request.BirthDate;
                    person.GenderId = request.GenderId;
                    person.DocumentTypeId = request.DocumentTypeId;
                    person.DocumentNumber = request.DocumentNumber;
                    person.CreationTime = DateTime.Now;
                    person.CreatorUserId = currentUserId;
                    person.IsActive = true;
                    person.IsDeleted = false;

                    db.People.Add(person);
                    db.SaveChanges();

                    //Update currentUser
                    var newPerson = db.People.OrderByDescending(x => x.Id).FirstOrDefault();

                    currentUser.PersonId = newPerson.Id;
                    db.SaveChanges();

                    response.Message = "Registro actualizado con exito";
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            response.Code = "008";
            response.Message = "Petición no procesada";
            return Ok(response);
        }


        [HttpPut]
        [Route("UpdateProfileImagen")]
        public IHttpActionResult UpdateProfileImagen([FromBody] UploadFile request)
        {
            var response = new Response(); 

            try
            {
                if (request != null)
                {
                    var fileTypeAlloweds = ConfigurationParameter.ImgTypeAllowed.Split(',');

                    string root = ConfigurationParameter.UserAvatarFileDirectory;
                    string[] arrayImgBase64 = request.base64.Split(',');
                    string imgBase64 = arrayImgBase64[arrayImgBase64.Length - 1];

                    string[] splitName1 = arrayImgBase64[0].Split('/');
                    string[] splitName2 = splitName1[1].Split(';');
                    string contentType = splitName2[0];

                    //Validate contentType
                    if (!fileTypeAlloweds.Contains(contentType))
                    {
                        response.Code = "400";
                        response.Message = "El tipo de imagen que intenta subir es desconocido, favor reemplacé la misma por otra";

                        return Ok(response);
                    }

                    var currentUser = db.Users.Where(x => x.Id == currentUserId).FirstOrDefault();

                    string defaultAvatar = ConfigurationParameter.UserAvataDefault;


                    //Save img
                    var guid = Guid.NewGuid();
                    var fileName = string.Concat("User_Avatar_Profile_", guid);
                    var filePath = Path.Combine(root, fileName) + "." + contentType;


                    if (currentUser.Image != defaultAvatar)
                    {
                        if (File.Exists(currentUser.Image))
                        {
                            File.Delete(currentUser.Image);
                        }
                    }

                    File.WriteAllBytes(filePath, Convert.FromBase64String(imgBase64));

                    currentUser.Image = filePath;
                    db.SaveChanges();

                    response.Message = "Imagen actualizada con exito";
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return Ok(response);
        }




        #region Models

        public class InfoCurrentUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string SurName { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
        }

        public class InfoCurrentPerson
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public string SurName { get; set; }
            public string SecondSurname { get; set; }
            public string BirthDate { get; set; }
            public string FullName { get; set; }
            public int GenderId { get; set; }
            public int? DocumentTypeId { get; set; }
            public string DocumentNumber { get; set; }
        }

        #endregion

    }
}
