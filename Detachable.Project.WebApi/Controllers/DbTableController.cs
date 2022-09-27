using Detachable.Project.Entity.Model;
using Detachable.Project.IBusiness.Foundation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Detachable.Project.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DbTableController : ControllerBase
    {
        private readonly IDbTableBusiness _dbBusiness;
        public DbTableController(IDbTableBusiness dbBusiness)
        {
            _dbBusiness = dbBusiness;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]DbTable dbTable)
        {
            dbTable.Guid = Guid.NewGuid().ToString();
            var result = await _dbBusiness.AddAsync(dbTable);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _dbBusiness.GetListAsync();
            return Ok(result);
        }
    }
}
