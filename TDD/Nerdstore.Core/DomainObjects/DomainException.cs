using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerdstore.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException() : base() { }
        public DomainException(string msg) : base(msg) { }
        public DomainException(string msg, Exception innerException) : base(msg, innerException) { }
        
    }
}
