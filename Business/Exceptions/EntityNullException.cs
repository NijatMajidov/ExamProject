using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class EntityNullException : Exception
    {
        public string PropertyName { get; set; }
        public EntityNullException(string name,string? message) : base(message)
        {
            PropertyName= name;
        }
    }
}
