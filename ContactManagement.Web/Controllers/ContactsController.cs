using ContactManagement.BLL;
using ContactManagement.Core;
using ContactManagement.Model;
using ContactManagement.Shared;
using ContactManagement.Web.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools;
using System.Collections;
using Newtonsoft.Json;
using System.Text;
using System.Data;

namespace ContactManagement.Web.Controllers
{
    [RoutePrefix("api/Contacts")]
    public class ContactsController : ApiController
    {

        [Route("getcenters")]
        [HttpGet]
        public IHttpActionResult GetContacts(string userName)
        {
            var Result = ContactService.GetContacts(userName);
            //return Ok(new ResponseMessage<List<ContactModels>>()
            //{
            //    Result = ContactService.GetContacts(userName)
            //});
            return Ok(Result);
        }
        [Route("save")]
        [HttpPost]
        public IHttpActionResult SaveContact([FromBody]ContactModels model)
        {
            return Ok(new ResponseMessage<ContactModels>()
            {
                Result = ContactService.SaveContact(model.ContactId, model)
            });

        }

        [Route("delete")]
        [HttpDelete]
        public IHttpActionResult DeleteContact(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = ContactService.DeleteContact(id)
            });

        }
        
    }
}
