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
    /// 班级信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<ClassController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public ClassController(IRepositoryWrapper dbContext,ILogger<ClassController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 获取班级信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetClassAsync()
        {
            try
            {
                IEnumerable<ClassInfo> classInfos = await dbContext.classInfoRepository.GetAllInfoAsync();
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示班级信息");
                return Ok(classInfos.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据Id获取到一条信息
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstClassAsync(int ClassId)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.classInfoRepository.IsExistAsync(ClassId))
                {
                    //找到这条数据
                    ClassInfo classInfo = await dbContext.classInfoRepository.GetFirstInfo(ClassId);
                    return Ok(classInfo);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示一条班级信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 添加班级信息
        /// </summary>
        /// <param name="classInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertClassAsync(ClassInfo classInfo)
        {
            try
            {
                dbContext.classInfoRepository.CreateInfo(classInfo);
                if (await dbContext.classInfoRepository.SaveAsync())
                    return Ok(1);
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}添加班级信息");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除一条班级信息
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteClassAsync(int classId)
        {
            try
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
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}删除班级信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="classInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateClassAsync(ClassInfo classInfo)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.classInfoRepository.IsExistAsync(classInfo.ClassID))
                {
                    //找到这条数据
                    ClassInfo info = await dbContext.classInfoRepository.GetFirstInfo(classInfo.ClassID);
                    //修改数据
                    info.ClassName = classInfo.ClassName;
                    dbContext.classInfoRepository.UpdateInfo(info);
                    if (await dbContext.classInfoRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}修改班级信息");
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
