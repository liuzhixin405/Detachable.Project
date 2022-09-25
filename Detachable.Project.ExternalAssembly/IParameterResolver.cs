using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Detachable.Project.ExternalAssembly
{
    internal interface IParameterResolver
    {
        object Resolve(object value);
    }

    public abstract class ParameterBinderAttribute : Attribute, IParameterResolver
    {
        public abstract object Resolve(object value);
    }

    public class ObjectAttribute : ParameterBinderAttribute
    {
        public Type TargetType { get; set; }

        public override object Resolve(object value)
        {
            if (TargetType == null || value == null)
            {
                return value;
            }

            if (TargetType == value.GetType())
            {
                return value;
            }

            if (TargetType.IsInstanceOfType(value))
            {
                return value;
            }

            return null;
        }


    }
    public class PropertyAttribute : ParameterBinderAttribute
    {
        public string Name { get; set; }

        public override object Resolve(object value)
        {
            if (value == null || Name == null)
            {
                return null;
            }

            var property = value.GetType().GetProperty(Name);
            return property.GetValue(value);
            //return property?.GetReflector()?.GetValue(value);
        }
    }
    public class NullParameterResolver : IParameterResolver
    {
        public object Resolve(object value)
        {
            return null;
        }
    }
}