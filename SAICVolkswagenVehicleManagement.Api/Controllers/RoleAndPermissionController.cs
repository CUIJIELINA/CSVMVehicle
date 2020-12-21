using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SAICVolkswagenVehicleManagement.Api.Models;
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
            try
            {
                //获取角色信息
                IEnumerable<RoleInfo> roleInfos = await dbContext.roleInfoRepository.GetAllInfoAsync();
                //获取菜单信息
                IEnumerable<Permission> permissions = await dbContext.permissionRepository.GetAllInfoAsync();
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示角色和菜单信息");
                //两表联查
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// 获取一条信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstRolePermissionAsync(int connectionId)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.role_PermissionRepository.IsExistAsync(connectionId))
                {
                    //找到这条数据
                    Role_Permission role_Permission = await dbContext.role_PermissionRepository.GetFirstInfo(connectionId);
                    return Ok(role_Permission);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示一条角色和菜单信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// 添加角色和菜单表
        /// </summary>
        /// <param name="role_Permission"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertRoleAndPermissionAsync(Role_Permission role_Permission)
        {
            try
            {
                dbContext.role_PermissionRepository.CreateInfo(role_Permission);
                if (await dbContext.role_PermissionRepository.SaveAsync())
                    return Ok(1);
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}添加角色和菜单信息");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRoleAndPermissionAsync(int connectionId)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.role_PermissionRepository.IsExistAsync(connectionId))
                {
                    //找到这条数据
                    Role_Permission role_Permission = await dbContext.role_PermissionRepository.GetFirstInfo(connectionId);
                    //删除数据
                    dbContext.role_PermissionRepository.DeleteInfo(role_Permission);
                    if (await dbContext.role_PermissionRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}删除角色和菜单信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// 修改选中的当前信息
        /// </summary>
        /// <param name="role_Permission"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoleAndPermissionAsync(Role_Permission role_Permission)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.role_PermissionRepository.IsExistAsync(role_Permission.ConnectionID))
                {
                    //找到这条数据
                    Role_Permission _Permission = await dbContext.role_PermissionRepository.GetFirstInfo(role_Permission.ConnectionID);
                    //修改数据信息
                    _Permission.RoleID = role_Permission.RoleID;
                    _Permission.PermissionID = role_Permission.PermissionID;
                    //保存当前修改的数据
                    dbContext.role_PermissionRepository.UpdateInfo(_Permission);
                    if (await dbContext.role_PermissionRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}修改角色和菜单信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 同时添加角色和权限信息
        /// </summary>
        /// <param name="model">添加的所有数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddRoleAndPermissionAsync(RoleAndPermissionDto model)
        {
            try
            {
                DateTime date = DateTime.Now;
                //分割获取到的权限Id，分为数组类型
                string[] PermissionIds = model.PermissionIds.Split(',');
                //实例化初始化器赋值
                RoleInfo roleInfo = new RoleInfo() { RoleName = model.Role_Name, CreateDate = date };
                //添加角色，返回受影响行数
                dbContext.roleInfoRepository.CreateInfo(roleInfo);
                if (await dbContext.roleInfoRepository.SaveAsync() == false)
                    throw new Exception("没有添加成功");
                //获取所有的角色列表
                IEnumerable<RoleInfo> role = await dbContext.roleInfoRepository.GetAllInfoAsync();
                //通过时间判断刚添加进去的数据
                RoleInfo info = role.ToList().Where(s => s.CreateDate.Equals(date)).FirstOrDefault();
                //赋值
                int code = info.RoleID;
                //循环添加角色权限表
                Role_Permission role_Permission = null;
                for (int i = 0; i < PermissionIds.Length; i++)
                {
                    role_Permission = new Role_Permission() { PermissionID = int.Parse(PermissionIds[i]), RoleID = code };
                    dbContext.role_PermissionRepository.CreateInfo(role_Permission);
                }
                if (await dbContext.role_PermissionRepository.SaveAsync())
                    return Ok(1);
                //记录日志
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}添加角色和权限");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
