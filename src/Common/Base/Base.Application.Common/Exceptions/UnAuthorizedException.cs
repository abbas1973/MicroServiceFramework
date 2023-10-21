using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class UnAuthorizedException : ApplicationException
    {
        public UnAuthorizedException(string message) : base(message)
        {

        }
    }
}
