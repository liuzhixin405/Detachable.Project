using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly
{
    internal class TracingDiagnosticMethod
    {
        private readonly ITracingDiagnosticProcessor _tracingDiagnosticProcessor;
        private readonly string _diagnosticName;
        private readonly IParameterResolver[] _parameterResolvers;
        //private readonly MethodReflector _reflector;
        private readonly MethodInfo _methodInfo;
        public string DiagnosticName => _diagnosticName;
        public TracingDiagnosticMethod(ITracingDiagnosticProcessor tracingDiagnosticProcessor, MethodInfo method,string diagnosticName)
        {
            _tracingDiagnosticProcessor = tracingDiagnosticProcessor;
            //_reflector = method.GetReflector();
            _diagnosticName = diagnosticName;
            _parameterResolvers = GetParameterResolvers(method).ToArray();
            _methodInfo = method;
        }

        public void Invoke(string diagnosticName,object value)
        {
            if (_diagnosticName != diagnosticName)
                return;
            var agrs = new object[_parameterResolvers.Length];
            for (int i = 0; i < _parameterResolvers.Length; i++)
            {
                agrs[i] = _parameterResolvers[i].Resolve(value);
            }
            _methodInfo.Invoke(_tracingDiagnosticProcessor, agrs);
            //_reflector.Invoke(_tracingDiagnosticProcessor,agrs);
        }
        private static IEnumerable<IParameterResolver> GetParameterResolvers(MethodInfo methodInfo)
        {
            foreach (var parameter in methodInfo.GetParameters())
            {
                var binder = parameter.GetCustomAttribute<ParameterBinderAttribute>();
                if (binder != null)
                {
                    if (binder is ObjectAttribute objectBinder)
                    {
                        if (objectBinder.TargetType == null)
                        {
                            objectBinder.TargetType = parameter.ParameterType;
                        }
                    }
                    if (binder is PropertyAttribute propertyBinder)
                    {
                        if (propertyBinder.Name == null)
                        {
                            propertyBinder.Name = parameter.Name;
                        }
                    }
                    yield return binder;
                }
                else
                {
                    yield return new NullParameterResolver();
                }
            }
        }
    }
}
