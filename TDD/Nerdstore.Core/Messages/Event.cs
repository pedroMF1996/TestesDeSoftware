using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerdstore.Core.Messages
{
    public class Event : Message,
                         INotification   
    {
        public Event() : base(nameof(Event), Guid.NewGuid())
        {
        }
    }
}
