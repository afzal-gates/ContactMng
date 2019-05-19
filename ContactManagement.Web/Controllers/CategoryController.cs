using CategoryManagement.BLL;
using ContactManagement.Core;
using ContactManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CategoryManagement.Web.Controllers
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {

        [Route("getCategory")]
        [HttpGet]
        public IHttpActionResult GetCategorys()
        {
            var Result = CategoryService.GetCategorys();
            //return Ok(new ResponseMessage<List<CategoryModels>>()
            //{
            //    Result = CategoryService.GetCategorys(userName)
            //});
            return Ok(Result);
        }

        [Route("save")]
        [HttpPost]
        public IHttpActionResult SaveCategory([FromBody]CategoryModels model)
        {
            return Ok(new ResponseMessage<CategoryModels>()
            {
                Result = CategoryService.SaveCategory(model.CategoryId, model)
            });

        }

        [Route("delete")]
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = CategoryService.DeleteCategory(id)
            });

        }

    }
}
