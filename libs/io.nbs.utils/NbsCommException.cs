using System;
using System.Collections.Generic;
using System.Text;

namespace io.nbs.utils
{
    public class NbsCommException : Exception
    {
        public NbsCommException(string message) : base(message)
        {
        }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }

        public NbsCommException(string message, Exception inner) : base(message, inner) { }

        public NbsCommException() { }
    }
}
