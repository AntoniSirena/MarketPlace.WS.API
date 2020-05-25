using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.DTO.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Services
{
    public class UserService
    {
        MyDBcontext db = new MyDBcontext();

        public List<UserStatus> GetUserStatuses()
        {
            var result = db.UserStatus.Select(x => new UserStatus
            {
                Id = x.Id,
                ShortName = x.ShortName,
                Description = x.Description,
                Colour = x.Colour,
            }).ToList();

            return result;
        }

        public UserDetails GetUserDetails(long userId)
        {
            var result = new UserDetails();

            var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();

            if (user.PersonId != null)
            {
                result = db.Users.Where(x => x.Id == userId).Select(x => new UserDetails
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Name = x.Name,
                    Surname = x.Surname,
                    EmailAddress = x.EmailAddress,
                    Image = x.Image,
                    Person = db.People.Where(y => y.Id == x.PersonId).Select(y => new Person
                    {
                        FirstName = y.FirstName,
                        SecondName = y.SecondName,
                        Surname = y.Surname,
                        secondSurname = y.secondSurname,
                        FullName = y.FullName,
                        BirthDate = y.BirthDate,
                        Gender = y.Gender.Description,
                        Locators = db.Locators.Where(z => z.PersonId == y.Id).Select(z => new Locators
                        {
                            Description = z.Description,
                            Type = z.LocatorType.Description,
                            IsMain = z.IsMain,
                        }).ToList()

                    }).FirstOrDefault()

                }).FirstOrDefault();
            }
            else
            {
                result = new UserDetails
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    Surname = user.Surname,
                    EmailAddress = user.EmailAddress,
                    Image = user.Image,
                    Person = new Person
                    {
                        FirstName = string.Empty,
                        SecondName = string.Empty,
                        Surname = string.Empty,
                        secondSurname = string.Empty,
                        FullName = string.Empty,
                        BirthDate = string.Empty,
                        Gender = string.Empty,
                        Locators = new List<Locators>()
                        {
                            new Locators
                            {
                               Description = string.Empty,
                               Type = string.Empty,
                               IsMain = false
                            }
                        }
                    },

                };
            }




            return result;
        }
    }
}