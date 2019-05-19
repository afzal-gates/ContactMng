using System;
using System.Net;

namespace ContactManagement.Shared
{
    [Serializable]
    public class MultiTexInternalServerException : MultiTexException
    {
        public MultiTexInternalServerException(string message)
            : base(message, (int)HttpStatusCode.InternalServerError)
        {
        }

        /// <summary>
        /// Optional Constructor that takes input for a String Format
        /// </summary>
        /// <param name="format">A string to format</param>
        /// <param name="args">input arguments for formatting the string</param>
        public MultiTexInternalServerException(string format, params object[] args)
            : base(string.Format(format, args), (int)HttpStatusCode.InternalServerError)
        {
        }

    }
}
