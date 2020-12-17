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
    /// 菜单信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<MenuController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public MenuController(IRepositoryWrapper dbContext,ILogger<MenuController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPermissionAsync()
        {
            try
            {
                IEnumerable<Permission> permissions = await dbContext.permissionRepository.GetAllInfoAsync();
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示菜单信息");
                return Ok(permissions.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取单条信息
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstPermissionAsync(int permissionId)
        {
            try
            {
                //判断传过来的信息是否存在
                if (await dbContext.permissionRepository.IsExistAsync(permissionId))
                {
                    //找到这条数据
                    Permission permission = await dbContext.permissionRepository.GetFirstInfo(permissionId);
                    return Ok(permission);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示一条菜单信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 添加菜单信息
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertPermissionAsync(Permission permission)
        {
            try
            {
                dbContext.permissionRepository.CreateInfo(permission);
                if (await dbContext.permissionRepository.SaveAsync())
                    return Ok(1);
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}添加菜单信息");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeletePermissionAsync(int permissionId)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.permissionRepository.IsExistAsync(permissionId))
                {
                    //找到这条数据
                    Permission permission = await dbContext.permissionRepository.GetFirstInfo(permissionId);
                    //删除数据
                    dbContext.permissionRepository.DeleteInfo(permission);
                    if (await dbContext.permissionRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}删除菜单信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePermissionAsync(Permission permission)
        {
            try
            {
                //判断传过来的信息是否存在
                if (await dbContext.permissionRepository.IsExistAsync(permission.PermissionID))
                {
                    //找到这一条数据
                    Permission per = await dbContext.permissionRepository.GetFirstInfo(permission.PermissionID);
                    //修改数据
                    dbContext.permissionRepository.UpdateInfo(per);
                    if (await dbContext.permissionRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}修改菜单信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}