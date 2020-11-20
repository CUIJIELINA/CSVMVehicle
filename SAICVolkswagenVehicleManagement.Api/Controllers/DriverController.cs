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
    /// 驾驶员信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public DriverController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// 获取驾驶员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  async Task<IActionResult> GetDriverInfoAsync()
        {
            IEnumerable<DriverInfo> driverInfos = await dbContext.driverInfoRepository.GetAllInfoAsync();
            return Ok(driverInfos.ToList());
        }

        /// <summary>
        /// 找到一条驾驶员信息
        /// </summary>
        /// <param name="driverId">驾驶员Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstDriver(int driverId)
        {
            //判断传过来的值是否存在
            if(await dbContext.driverInfoRepository.IsExistAsync(driverId))
            {
                //找到这一条数据
                DriverInfo driverInfo = await dbContext.driverInfoRepository.GetFirstInfo(driverId);
                return Ok(driverInfo);
            }
            //如果没有找到，返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 添加驾驶员
        /// </summary>
        /// <param name="driverInfo">驾驶员信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertDriverAsync(DriverInfo driverInfo)
        {
            dbContext.driverInfoRepository.CreateInfo(driverInfo);
            if(await dbContext.driverInfoRepository.SaveAsync())
            {
                return Ok(1);
            }
            return Ok("添加失败");
        }

        /// <summary>
        /// 删除对应的一条驾驶员信息
        /// </summary>
        /// <param name="driverId">驾驶员Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteDriverAsync(int driverId)
        {
            //判断传过来的值是否存在
            if(await dbContext.driverInfoRepository.IsExistAsync(driverId))
            {
                //如果存在找到这条数据
                DriverInfo driverInfo = await dbContext.driverInfoRepository.GetFirstInfo(driverId);
                //删除数据
                dbContext.driverInfoRepository.DeleteInfo(driverInfo);
                if(await dbContext.driverInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //如果不存在，返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 修改一条驾驶员信息
        /// </summary>
        /// <param name="driverInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateDriverAsync(DriverInfo driverInfo)
        {
            //判断传过来的值是否存在
            if(await dbContext.driverInfoRepository.IsExistAsync(driverInfo.DriverID))
            {
                //找到这条数据
                DriverInfo driver = await dbContext.driverInfoRepository.GetFirstInfo(driverInfo.DriverID);
                dbContext.driverInfoRepository.UpdateInfo(driver);
                if(await dbContext.driverInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //如果没有找到返回错误信息
            return NotFound();
        }
    }
}
