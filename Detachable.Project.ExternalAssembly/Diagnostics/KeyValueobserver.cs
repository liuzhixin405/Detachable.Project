using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.ExternalAssembly.Diagnostics
{
    internal class KeyValueObserver : IObserver<KeyValuePair<string, object>>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly int _minCommandElapsedMilliseconds;
           // = new ConcurrentDictionary<Guid, StackTrace>();
        public KeyValueObserver(
            ILoggerFactory loggerFactory, int minCommandElapsedMilliseconds
            )
        {
            _loggerFactory = loggerFactory;
            _minCommandElapsedMilliseconds = minCommandElapsedMilliseconds;
        }
        public void OnCompleted()
            => throw new NotImplementedException();

        public void OnError(Exception error)
            => throw new NotImplementedException();

        public void OnNext(KeyValuePair<string, object> value)
        {
            var logger = _loggerFactory?.CreateLogger(GetType());
            logger.Log(LogLevel.Information,value.Key,value.Value);
        }
    }
}
