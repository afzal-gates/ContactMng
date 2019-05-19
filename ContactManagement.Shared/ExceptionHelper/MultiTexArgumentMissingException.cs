using System;
using System.Net;

namespace ContactManagement.Shared
{
    [Serializable]
    public class MultiTexArgumentMissingException : MultiTexException
    {
        public MultiTexArgumentMissingException(string message)
            : base(message, (int)HttpStatusCode.BadRequest)
        {
        }

        public MultiTexArgumentMissingException(string format, params object[] args)
            : this(string.Format(format, args))
        {
        }
    }
}
