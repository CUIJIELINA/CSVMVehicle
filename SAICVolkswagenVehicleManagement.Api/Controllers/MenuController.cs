﻿using System;
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

        /// <summary>
        /// 获取单条信息
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstPermissionAsync(int permissionId)
        {
            //判断传过来的信息是否存在
            if(await dbContext.permissionRepository.IsExistAsync(permissionId))
            {
                //找到这条数据
                Permission permission = await dbContext.permissionRepository.GetFirstInfo(permissionId);
                return Ok(permission);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 添加菜单信息
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertPermissionAsync(Permission permission)
        {
            dbContext.permissionRepository.CreateInfo(permission);
            if(await dbContext.permissionRepository.SaveAsync())
            {
                return Ok(1);
            }
            return Ok("添加失败");
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeletePermissionAsync(int permissionId)
        {
            //判断传过来的值是否存在
            if(await dbContext.permissionRepository.IsExistAsync(permissionId))
            {
                //找到这条数据
                Permission permission = await dbContext.permissionRepository.GetFirstInfo(permissionId);
                //删除数据
                dbContext.permissionRepository.DeleteInfo(permission);
                if(await dbContext.permissionRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePermissionAsync(Permission permission)
        {
            //判断传过来的信息是否存在
            if(await dbContext.permissionRepository.IsExistAsync(permission.PermissionID))
            {
                //找到这一条数据
                Permission per = await dbContext.permissionRepository.GetFirstInfo(permission.PermissionID);
                //修改数据
                dbContext.permissionRepository.UpdateInfo(per);
                if(await dbContext.permissionRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //如果不存在返回错误信息
            return NotFound();
        }
    }
}