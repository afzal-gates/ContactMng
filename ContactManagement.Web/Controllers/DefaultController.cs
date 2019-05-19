using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactManagement.Web.Controllers
{
    [RoutePrefix("api/abc")]
    public class DefaultController : ApiController
    {
        [Route("xyz")]
        [HttpGet]
        // GET api/abc
        public IHttpActionResult Get()
        {
            List<myList> mList = new List<myList>()
            {
                new myList {id="1",name1="dsdfdf",name2="zxcvcsdfsdf" },
                new myList {id="2",name1="dfgfgrrt",name2="werwer" },
                new myList {id="3",name1="rerr",name2="xzcxvcxv" },
                new myList {id="4",name1="ytytyyt",name2="cvbcbv" }
            };
            return Ok(mList);
        }

        [Route("save")]
        [HttpPost]
        // GET api/abc
        public IHttpActionResult Save([FromBody] myList obj)
        {
            obj.name1 = "Success";
            return Ok(obj.name1);
        }

    }
    public class myList
    {
        public string id { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
    }
}
