using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace ContactManagement.Core
{
    [Authorize]
    [ValidationFilter]
    public class BaseApiController : ApiController
    {
   
        public string UserId
        {
            get
            {
                var  contectxt = HttpContext.Current;
                if (contectxt != null)
                {
                  var  userId= contectxt.Session["ScUserId"].ToString();
                    if (userId != null)
                    {
                        return userId;
                    }
                }

                return string.Empty;
            }
        }
    }
}
