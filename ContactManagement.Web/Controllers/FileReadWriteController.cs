using ContactManagement.Model;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using System.Text;
using ContactManagement.BLL;
using System.Collections.Generic;

namespace ContactManagement.Web.Controllers
{
    [RoutePrefix("api/FileReadWrite")]
    public class FileReadWriteController : ApiController
    {
        
        [Route("UploadFile")]
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UploadFile()
        {
            var fileName = "";
            //Contact obj
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["file"];
                bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                var list = httpPostedFile.FileName.Split('.');
                fileName = Guid.NewGuid().ToString() + "." + list[list.Length - 1];

                if (!folderExists)
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedDocuments"),
                                                fileName);
                httpPostedFile.SaveAs(fileSavePath);
            }
            return Ok(new { success = true, imgUrl = fileName });
        }


        [Route("ExportCsv")]
        [HttpPost]
        public IHttpActionResult ExportCsv([FromBody] ContactModel objList)
        {
            string fileName = DateTime.Now.Ticks + ".csv";
            string filePath = HttpContext.Current.Server.MapPath(String.Format("~/UploadedDocuments/{0}", fileName));

            StringBuilder str = new StringBuilder();

            var list = objList.ContactList.ToList();
            var obj = list;

            str.Append("Name,Email,Mobile,Address,CategoryId,Category,ProfilePicture,UserName");
            str.Append("\n");

            foreach (ContactModels val in obj)
            {
                string s = val.Name + "," + val.Email + "," + val.MobileNo + "," + val.Address.Replace(',', ' ') + "," + val.CategoryId + "," + val.Title + "," + val.ProfilePicture+","+val.UserName;
                str.Append(s + "\n");
            }
            
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());

            // write the data into Excel file
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write(str.ToString());
            }

            return Ok(new { success = true, jsonStr = fileName });
            
        }


        [Route("ExportExcel")]
        [HttpPost]
        public IHttpActionResult ExportExcel([FromBody] ContactModel objList)
        {

            var response = new HttpResponseMessage();
            string fileName = DateTime.Now.Ticks + ".xls";
            string filePath = HttpContext.Current.Server.MapPath(String.Format("~/UploadedDocuments/{0}", fileName));

            StringBuilder str = new StringBuilder();

            var list = objList.ContactList.ToList();

            var obj = list;


            str.Append("<table border=`" + "1px" + "`b>");

            str.Append("<tr>");

            str.Append("<td><b><font face=Arial Narrow size=3>Name</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Email</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Mobile</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Address</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>CategoryId</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Category</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>ProfilePicture</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>UserName</font></b></td>");

            str.Append("</tr>");

            foreach (ContactModels val in obj)
            {
                str.Append("<tr>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Name.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Email.ToString() + "</font></td>");               
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.MobileNo.ToString() + "</font></td>");               
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Address.ToString() + "</font></td>");               
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.CategoryId.ToString() + "</font></td>");               
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.Title.ToString() + "</font></td>");               
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.ProfilePicture.ToString() + "</font></td>");               
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + val.UserName.ToString() + "</font></td>");

                str.Append("</tr>");
            }

            str.Append("</table>");
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());

            // write the data into Excel file
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write(str.ToString());
            }

            return Ok(new { success = true, jsonStr = fileName });
        }


        [Route("ImportContact")]
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult ImportContact(List<ContactModels> contactList)
        {
            foreach (var model in contactList)
            {
                ContactService.SaveContact(model.ContactId, model);
            }
            return Ok(new { success = true, jsonStr = "OK" });
        }
    }
}
