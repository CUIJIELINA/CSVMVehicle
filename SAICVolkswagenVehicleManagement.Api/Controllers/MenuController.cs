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
    /// 菜单信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public MenuController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPermissionAsync()
        {
            IEnumerable<Permission> permissions = await dbContext.permissionRepository.GetAllInfoAsync();
            return Ok(permissions.ToList());
        }
    }
}
