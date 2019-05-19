using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Shared
{
    [Serializable]
    public class MultiTexDatabaseException : MultiTexException
    {
        public MultiTexDatabaseException(string message)
            : base(message, (int)HttpStatusCode.InternalServerError)
        {
        }

        public MultiTexDatabaseException(string message, Exception innerException)
            : base(message, (int)HttpStatusCode.InternalServerError, innerException)
        {
        }

        public MultiTexDatabaseException(string format, params object[] args)
            : base(string.Format(format, args), (int)HttpStatusCode.InternalServerError)
        {
        }

        public MultiTexDatabaseException(Exception innerException, string format, params object[] args)
            : base(string.Format(format, args), (int)HttpStatusCode.InternalServerError, innerException)
        {
        }
    }
}
