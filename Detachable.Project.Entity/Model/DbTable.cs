using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.Entity.Model
{
    public class DbTable
    {
        public DbTable()
        {

        }
        /// <summary>
        /// 唯一编号
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; } = false;
        /// <summary>
        /// 表单创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    }
}
