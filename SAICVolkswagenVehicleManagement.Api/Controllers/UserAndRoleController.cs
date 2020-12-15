using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAICVolkswagenVehicleManagement.Api.Models;
using SAICVolkswagenVehicleManagement_Helper;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement.Api.Controllers
{
    /// <summary>
    /// 用户和角色信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserAndRoleController : ControllerBase
    {

        private readonly IRepositoryWrapper dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public UserAndRoleController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 获取用户和角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserAndRoleAsync()
        {
            //获取用户信息
            IEnumerable<R_UserInfo> userInfos = await dbContext.r_UserInfoRepository.GetAllInfoAsync();
            //获取角色信息
            IEnumerable<RoleInfo> roleInfos = await dbContext.roleInfoRepository.GetAllInfoAsync();
            return Ok();
        }

        /// <summary>
        /// 获取一条信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstUserAndRoleAsync(int connectionId)
        {
            //判断传过来的值是否存在
            if(await dbContext.user_RoleRepository.IsExistAsync(connectionId))
            {
                //找到这条数据
                User_Role user_Role = await dbContext.user_RoleRepository.GetFirstInfo(connectionId);
                return Ok(user_Role);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="user_Role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertUserAndRoleAsync(User_Role user_Role)
        {
            dbContext.user_RoleRepository.CreateInfo(user_Role);
            if(await dbContext.user_RoleRepository.SaveAsync())
                return Ok(1);
            return Ok("添加失败");
        }

        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAndRoleAsync(int connectionId)
        {
            //判断传过来的值是否存在
            if(await dbContext.user_RoleRepository.IsExistAsync(connectionId))
            {
                //找到这条数据
                User_Role user_Role = await dbContext.user_RoleRepository.GetFirstInfo(connectionId);
                //删除数据
                dbContext.user_RoleRepository.DeleteInfo(user_Role);
                if(await dbContext.user_RoleRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="user_Role"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUserAndRoleAsync(User_Role user_Role)
        {
            //判断传过来的数据是否存在
            if(await dbContext.user_RoleRepository.IsExistAsync(user_Role.ConnectionID))
            {
                //找到这条数据
                User_Role user = await dbContext.user_RoleRepository.GetFirstInfo(user_Role.ConnectionID);
                //修改数据
                dbContext.user_RoleRepository.UpdateInfo(user);
                if(await dbContext.user_RoleRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 同时添加用户和角色关联表
        /// </summary>
        /// <param name="model">用户和角色信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUserAndRoleAsync(UserAndRoleDto model)
        {
            //获取到用户信息
            IEnumerable<R_UserInfo> r_UserInfos = await dbContext.r_UserInfoRepository.GetAllInfoAsync();
            //根据名称查找到这一条数据
            R_UserInfo user = r_UserInfos.ToList().Where(s => s.UserName.Equals(model.User_Name)).FirstOrDefault();
            if(user == null)
                throw new Exception("没有找到这一条信息");
            User_Role m = new User_Role() { RoleID = model.RoleId, UserID = user.UserID };
            dbContext.user_RoleRepository.CreateInfo(m);
            if(await dbContext.user_RoleRepository.SaveAsync())
                return Ok(1);
            return Ok("添加失败");
        }
    }
}
