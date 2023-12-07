using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerdstore.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity() 
        {
            Id = Guid.NewGuid();
        }
    }
}
