﻿using System;
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
    /// 角色信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoleInfoController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<RoleInfoController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public RoleInfoController(IRepositoryWrapper dbContext,ILogger<RoleInfoController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoleInfoAsync()
        {
            try
            {
                IEnumerable<RoleInfo> roleInfos = await dbContext.roleInfoRepository.GetAllInfoAsync();
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示角色信息");
                return Ok(roleInfos.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取一条角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstRoleInfoAsync(int roleId)
        {
            try
            {
                //判断传过来的ID是否存在
                if (await dbContext.roleInfoRepository.IsExistAsync(roleId))
                {
                    //找到这条数据
                    RoleInfo roleInfo = await dbContext.roleInfoRepository.GetFirstInfo(roleId);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示一条角色信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="roleInfo">角色信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertRoleInfoAsync(RoleInfo roleInfo)
        {
            try
            {
                dbContext.roleInfoRepository.CreateInfo(roleInfo);
                if (await dbContext.roleInfoRepository.SaveAsync())
                    return Ok(1);
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}添加角色信息");
                return Ok("注册失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除一条角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRoleInfoAsync(int roleId)
        {
            try
            {
                //判断传过来的ID是否存在
                if (await dbContext.roleInfoRepository.IsExistAsync(roleId))
                {
                    //找到这条数据
                    RoleInfo roleInfo = await dbContext.roleInfoRepository.GetFirstInfo(roleId);
                    //删除信息
                    dbContext.roleInfoRepository.DeleteInfo(roleInfo);
                    if (await dbContext.roleInfoRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}删除角色信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleInfo">角色信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoleInfoAsync(RoleInfo roleInfo)
        {
            try
            {
                //判断传过来的信息是否存在
                if (await dbContext.roleInfoRepository.IsExistAsync(roleInfo.RoleID))
                {
                    //找到这条数据
                    RoleInfo role = await dbContext.roleInfoRepository.GetFirstInfo(roleInfo.RoleID);
                    //修改数据
                    role.RoleName = roleInfo.RoleName;
                    dbContext.roleInfoRepository.UpdateInfo(role);
                    if (await dbContext.roleInfoRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}修改角色信息");
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
