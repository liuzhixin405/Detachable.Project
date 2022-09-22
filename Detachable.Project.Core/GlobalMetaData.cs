using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.Core
{
    public static class GlobalMetaData
    {
        /// <summary>
        /// 解决方案程序集匹配名
        /// </summary>
        public const string FXASSEMBLY_PATTERN = "Detachable.Project";

        /// <summary>
        /// 解决方案所有程序集
        /// </summary>
        public static Assembly[] AllFxAssemblies
        {
            get
            {
                if (_assemblies == null)
                {
                    string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    _assemblies = Directory.GetFiles(rootPath, "*.dll")
                        .Where(x => new FileInfo(x).Name.Contains(FXASSEMBLY_PATTERN))
                        .Select(x => Assembly.LoadFrom(x))
                        .Where(x => !x.IsDynamic).ToArray();
                }
                return _assemblies;
            }
        }

        static Assembly[] _assemblies;
        /// <summary>
        /// 解决方案所有自定义类
        /// </summary>
        public static readonly Type[] AllTypes = AllFxAssemblies.SelectMany(x => x.GetTypes()).ToArray();
    }
}
