using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Base.WS.API.Services.IServices
{
    interface IFileDocument
    {
       void SaveFile(string name, string path, bool isPublic, string contentType);
    }
}
