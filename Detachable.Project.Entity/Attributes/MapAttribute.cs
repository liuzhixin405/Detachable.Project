using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.Entity.Attributes
{
    public class MapAttribute:Attribute
    {
        public MapAttribute(params Type[] targetTypes)
        {
            TargetTypes = targetTypes;
        }
        public Type[] TargetTypes { get; }
    }
}
