using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ContactManagement.Web.Controllers
{
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {

        [Route("UploadFile")]
        [HttpPost]
        [ResponseType(typeof(void))]
        public void UploadFile()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                var list = httpPostedFile.FileName.Split('.');
                string fileName = Guid.NewGuid().ToString() + "."+list[list.Length - 1];
                if (!folderExists)
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedDocuments"),
                                                fileName);
                httpPostedFile.SaveAs(fileSavePath);
                
            }
        }

    }
}
