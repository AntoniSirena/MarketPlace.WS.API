using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Helpers;
using JS.Base.WS.API.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Base.WS.API.Services
{
    public class FileDocument: IFileDocument
    {
        private MyDBcontext db;
        private long currentUserId = CurrentUser.GetId();

        public FileDocument()
        {
            db = new MyDBcontext();
        }


        public void SaveFile(string name, string path, bool isPublic)
        {
            var file = new Models.FileDocument.FileDocument()
            {
                Name = name,
                Path = path,
                IsPublic = isPublic,
                CreationTime = DateTime.Now,
                CreatorUserId = currentUserId,
                IsActive = true,            };

            var result = db.FileDocuments.Add(file);

            db.SaveChanges();
        }

    }
}