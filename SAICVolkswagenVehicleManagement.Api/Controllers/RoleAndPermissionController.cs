using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SAICVolkswagenVehicleManagement_Helper;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement.Api.Controllers
{
    /// <summary>
    /// 角色和菜单信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoleAndPermissionController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<RoleAndPermissionController> _logger;

        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public RoleAndPermissionController(IRepositoryWrapper dbContext,ILogger<RoleAndPermissionController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 显示角色和菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoleAndPermissionAsync()
        {
            //获取角色信息
            IEnumerable<RoleInfo> roleInfos = await dbContext.roleInfoRepository.GetAllInfoAsync();
            //获取菜单信息
            IEnumerable<Permission> permissions = await dbContext.permissionRepository.GetAllInfoAsync();
            //两表联查
            return Ok();
        }

        /// <summary>
        /// 获取一条信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstRolePermissionAsync(int connectionId)
        {
            //判断传过来的值是否存在
            if(await dbContext.role_PermissionRepository.IsExistAsync(connectionId))
            {
                //找到这条数据
                Role_Permission role_Permission = await dbContext.role_PermissionRepository.GetFirstInfo(connectionId);
                return Ok(role_Permission);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 添加角色和菜单表
        /// </summary>
        /// <param name="role_Permission"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertRoleAndPermissionAsync(Role_Permission role_Permission)
        {
            dbContext.role_PermissionRepository.CreateInfo(role_Permission);
            if(await dbContext.role_PermissionRepository.SaveAsync())
                return Ok(1);
            return Ok("添加失败");
        }

        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRoleAndPermissionAsync(int connectionId)
        {
            //判断传过来的值是否存在
            if(await dbContext.role_PermissionRepository.IsExistAsync(connectionId))
            {
                //找到这条数据
                Role_Permission role_Permission = await dbContext.role_PermissionRepository.GetFirstInfo(connectionId);
                //删除数据
                dbContext.role_PermissionRepository.DeleteInfo(role_Permission);
                if(await dbContext.role_PermissionRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 修改选中的当前信息
        /// </summary>
        /// <param name="role_Permission"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoleAndPermissionAsync(Role_Permission role_Permission)
        {
            //判断传过来的数据是否存在
            if(await dbContext.role_PermissionRepository.IsExistAsync(role_Permission.ConnectionID))
            {
                //找到这条数据
                Role_Permission _Permission = await dbContext.role_PermissionRepository.GetFirstInfo(role_Permission.ConnectionID);
                //修改数据信息
                _Permission.RoleID = role_Permission.RoleID;
                _Permission.PermissionID = role_Permission.PermissionID;
                //保存当前修改的数据
                dbContext.role_PermissionRepository.UpdateInfo(_Permission);
                if(await dbContext.role_PermissionRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }
    }
}
