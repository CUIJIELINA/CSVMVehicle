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
    /// 班级信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public ClassController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 获取班级信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetClassAsync()
        {
            IEnumerable<ClassInfo> classInfos = await dbContext.classInfoRepository.GetAllInfoAsync();
            return Ok(classInfos.ToList());
        }

        /// <summary>
        /// 根据Id获取到一条信息
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstClassAsync(int ClassId)
        {
            //判断传过来的值是否存在
            if(await dbContext.classInfoRepository.IsExistAsync(ClassId))
            {
                //找到这条数据
                ClassInfo classInfo = await dbContext.classInfoRepository.GetFirstInfo(ClassId);
                return Ok(classInfo);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 添加班级信息
        /// </summary>
        /// <param name="classInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertClassAsync(ClassInfo classInfo)
        {
            dbContext.classInfoRepository.CreateInfo(classInfo);
            if(await dbContext.classInfoRepository.SaveAsync())
                return Ok(1);
            return Ok("添加失败");
        }

        /// <summary>
        /// 删除一条班级信息
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteClassAsync(int classId)
        {
            //判断传过来的值是否存在
            if (await dbContext.classInfoRepository.IsExistAsync(classId))
            {
                //找到这条数据
                ClassInfo classInfo = await dbContext.classInfoRepository.GetFirstInfo(classId);
                //删除数据
                dbContext.classInfoRepository.DeleteInfo(classInfo);
                if (await dbContext.classInfoRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="classInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateClassAsync(ClassInfo classInfo)
        {
            //判断传过来的值是否存在
            if(await dbContext.classInfoRepository.IsExistAsync(classInfo.ClassID))
            {
                //找到这条数据
                ClassInfo info = await dbContext.classInfoRepository.GetFirstInfo(classInfo.ClassID);
                //修改数据
                info.ClassName = classInfo.ClassName;
                dbContext.classInfoRepository.UpdateInfo(info);
                if (await dbContext.classInfoRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }
    }
}
