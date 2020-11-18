using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAICVolkswagenVehicleManagement_Helper;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement.Api.Controllers
{
    /// <summary>
    /// 部门信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public DepartmentController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDepartmentAsync()
        {
            IEnumerable<DepartmentInfo> departmentInfos = await dbContext.departmentInfoRepository.GetAllInfoAsync();
            return Ok(departmentInfos.ToList());
        }
    }
}
