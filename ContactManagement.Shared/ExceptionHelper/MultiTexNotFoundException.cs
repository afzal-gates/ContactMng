using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Shared
{
    [Serializable]
    public class MultiTexNotFoundException : MultiTexException
    {
        public MultiTexNotFoundException(string message)
            : base(message, (int)HttpStatusCode.NotFound)
        {
        }

        public MultiTexNotFoundException(string format, params object[] args)
            : this(string.Format(format, args))
        {
        }
    }
}
