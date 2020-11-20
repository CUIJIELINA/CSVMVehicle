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
    /// 用户信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_dbContext"></param>
        public UserInfoController(IRepositoryWrapper _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserInfoAsync()
        {
            //获取员工信息表
            IEnumerable<R_UserInfo> r_UserInfos = await dbContext.r_UserInfoRepository.GetAllInfoAsync();
            //获取部门列表
            IEnumerable<DepartmentInfo> departmentInfos = await dbContext.departmentInfoRepository.GetAllInfoAsync();
            //两表联查
            var list = (from r in r_UserInfos.ToList()
                        join d in departmentInfos.ToList()
                        on r.Department_ID equals d.DepartmentID
                        select new UserAndDepartmentDto()
                        {
                            UserID = r.UserID,
                            Department_ID = d.DepartmentID,
                            UserName = r.UserName,
                            DepartmentName = d.DepartmentName,
                            UserCode = r.UserCode,
                            UserPassWord = r.UserPassWord,
                            DepartmentCode = d.DepartmentCode,
                            Birthday = r.Birthday,
                            CreateDate = r.CreateDate,
                            E_Mail = r.E_Mail,
                            UserRemark = r.UserRemark,
                            UserSex = r.UserSex
                        }).ToList();
            return Ok(list);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="r_UserInfo">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginUserInfoAsync(R_UserInfo r_UserInfo)
        {
            //判断传过来的值是否存在
            if (await dbContext.r_UserInfoRepository.IsExistAsync(r_UserInfo.UserID))
            {
                //找到这一条userInfo的数据
                R_UserInfo userInfo = await dbContext.r_UserInfoRepository.GetFirstInfo(r_UserInfo.UserID);
                //判断登录用户名和密码
                if (r_UserInfo.UserName == userInfo.UserName && r_UserInfo.UserPassWord == userInfo.UserPassWord)
                {
                    return Ok(1);
                }
            }
            //如果没有找到说明数据不存在
            return NotFound();
        }

        /// <summary>
        /// 找到一条数据
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstUserInfoAsync(int userId)
        {
            //判断传过来的值是否存在
            if (await dbContext.r_UserInfoRepository.IsExistAsync(userId))
            {
                R_UserInfo r_UserInfo = await dbContext.r_UserInfoRepository.GetFirstInfo(userId);
                return Ok(r_UserInfo);
            }
            //如果没有说明数据不存在
            return NotFound();
        }

        /// <summary>
        /// 注册员工信息
        /// </summary>
        /// <param name="r_UserInfo"></param>
        /// <returns>注册员工信息</returns>
        [HttpPost]
        public async Task<IActionResult> InsertUserInfoAsync(R_UserInfo r_UserInfo)
        {
            dbContext.r_UserInfoRepository.CreateInfo(r_UserInfo);
            if (await dbContext.r_UserInfoRepository.SaveAsync())
            {
                return Ok(1);
            }
            return Ok("注册失败");
        }

        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="userId">员工ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteUserInfoAsync(int userId)
        {
            //判断传过来的值是否存在
            if (await dbContext.r_UserInfoRepository.IsExistAsync(userId))
            {
                //找到这条数据
                R_UserInfo userInfo = await dbContext.r_UserInfoRepository.GetFirstInfo(userId);
                dbContext.r_UserInfoRepository.DeleteInfo(userInfo);
                if (await dbContext.r_UserInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            return NotFound();
        }

        /// <summary>
        /// 修改一条用户数据
        /// </summary>
        /// <param name="userInfo">反填的用户信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUserInfoAsync(R_UserInfo userInfo)
        {
            //判断穿过来的值是否存在
            if (await dbContext.r_UserInfoRepository.IsExistAsync(userInfo.UserID))
            {
                //找到这条数据
                R_UserInfo r_UserInfo = await dbContext.r_UserInfoRepository.GetFirstInfo(userInfo.UserID);
                r_UserInfo.UserName = userInfo.UserName;
                r_UserInfo.UserPassWord = userInfo.UserPassWord;
                r_UserInfo.UserCode = userInfo.UserCode;
                r_UserInfo.UserSex = userInfo.UserSex;
                r_UserInfo.CreateDate = DateTime.Now;
                r_UserInfo.Department_ID = userInfo.Department_ID;
                r_UserInfo.E_Mail = userInfo.E_Mail;
                dbContext.r_UserInfoRepository.UpdateInfo(r_UserInfo);
                if (await dbContext.r_UserInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //如果不存在
            return NotFound();
        }
    }
}