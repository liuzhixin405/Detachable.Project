using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.Core.Abstraction
{
    public class CastleInterceptor : AsyncInterceptorBase
    {
        private readonly IServiceProvider _serviceProvider;
        public CastleInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private IAOPContext _aopContext;
        private List<BaseAOPAttribute> _aops;
        private async Task Before()
        {
            foreach (var aop in _aops)
            {
               await aop.Before(_aopContext);
            }
        }

        private async Task After()
        {
            foreach (var aop in _aops)
            {
                await aop.After(_aopContext);
            }
        }

        private void Init(IInvocation invocation)
        {
            _aopContext = new CastleAOPContext(invocation, _serviceProvider);
            _aops = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(BaseAOPAttribute), true)
                .Concat(invocation.InvocationTarget.GetType().GetCustomAttributes(typeof(BaseAOPAttribute), true))
                .Select(x => (BaseAOPAttribute)x).ToList();
        }
        protected override async Task InterceptAsync(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task> proceed)
        {
            Init(invocation);
            await Before();
            await proceed(invocation, proceedInfo);
            await After();
        }

        protected async override Task<TResult> InterceptAsync<TResult>(IInvocation invocation, IInvocationProceedInfo proceedInfo, Func<IInvocation, IInvocationProceedInfo, Task<TResult>> proceed)
        {
            Init(invocation);
            TResult result;
           
            await Before();
            result = await proceed(invocation, proceedInfo);
            await After();
            return result;
        }
    }
}
