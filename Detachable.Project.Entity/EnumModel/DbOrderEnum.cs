using Detachable.Project.Core.Extensions;
using Detachable.Project.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.Entity.EnumModel
{
    public enum DbOrderEnum
    {
        /// <summary>
        /// 打折
        /// </summary>
        [Text("排序Asc")]
        Asc = 1,

        /// <summary>
        /// 满减
        /// </summary>
        [Text("排序Desc")]
        Desc = 2
    }
}
