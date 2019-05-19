using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Shared
{
    [Serializable]
    public class MultiTexInvalidDataException : MultiTexException
    {
        public MultiTexInvalidDataException(string message)
            : base(message, (int)HttpStatusCode.BadRequest)
        {
        }

        public MultiTexInvalidDataException(string format, params object[] args) : 
            this(string.Format(format, args))
        {

        }
    }
}
