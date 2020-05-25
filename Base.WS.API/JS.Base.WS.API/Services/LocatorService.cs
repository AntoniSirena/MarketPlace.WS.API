using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Services
{
    public class LocatorService : ILocatorService
    {
        private long currentUserId = CurrentUser.GetId();
        private MyDBcontext db;

        public LocatorService()
        {
            db = new MyDBcontext();
        }

        public bool updateLocatorIsMainFalse()
        {
            bool result = true;

            try
            {
                var user = db.Users.Where(x => x.Id == currentUserId).FirstOrDefault();

                var locators = db.Locators.Where(x => x.PersonId == user.PersonId).ToList();

                foreach (var item in locators)
                {
                    item.IsMain = false;
                }
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return result = false;
            }

            return result;
        }
    }
}