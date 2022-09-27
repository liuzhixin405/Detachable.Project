using Detachable.Project.Core.IService;
using Detachable.Project.DataBase;
using Detachable.Project.Entity.Model;
using Detachable.Project.IBusiness.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detachable.Project.Business.Foundation
{
    public class DbTableBusiness:BaseService<DbTable>,IDbTableBusiness,ITransientDependency
    {

    }
}
