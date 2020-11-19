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

        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertDepartmentAsync(DepartmentInfo departmentInfo)
        {
            dbContext.departmentInfoRepository.CreateInfo(departmentInfo);
            if(await dbContext.departmentInfoRepository.SaveAsync())
            {
                return Ok(1);
            }
            return Ok("添加失败");
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartmentAsync(int departmentId)
        {
            //首先查找一条数据
            if(await dbContext.departmentInfoRepository.IsExistAsync(departmentId))
            {
                //找出对应的一条数据
                DepartmentInfo departmentInfo = await dbContext.departmentInfoRepository.GetFirstInfo(departmentId);
                dbContext.departmentInfoRepository.DeleteInfo(departmentInfo);
                if(await dbContext.departmentInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //没有找到这条数据
            return NotFound();
        }

        /// <summary>
        /// 找到一条部门信息
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstDepartmentAsync(int departmentId)
        {
            //判断传过来的ID是否存在
            if(await dbContext.departmentInfoRepository.IsExistAsync(departmentId))
            {
                //找到这一条数据
                DepartmentInfo departmentInfo = await dbContext.departmentInfoRepository.GetFirstInfo(departmentId);
                return Ok(departmentInfo);
            }
            //没有找到数据
            return NotFound();
        }
    }
}
