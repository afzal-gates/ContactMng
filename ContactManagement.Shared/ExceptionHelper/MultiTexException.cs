using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Shared
{
    [Serializable]
    public class MultiTexException : Exception
    {
        public MultiTexException(string message, int errorCode)
            : base(message)
        {
            HResult = errorCode;
        }

        public MultiTexException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            HResult = errorCode;
        }
    }
}
