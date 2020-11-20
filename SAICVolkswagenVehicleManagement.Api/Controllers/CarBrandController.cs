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
    /// 车辆品牌信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class CarBrandController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public CarBrandController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 获取车辆品牌信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCarBrandAsync()
        {
            IEnumerable<CarBrandInfo> carBrandInfos = await dbContext.carBrandInfoRepository.GetAllInfoAsync();
            return Ok(carBrandInfos.ToList());
        }

        /// <summary>
        /// 根据Id获取一条信息
        /// </summary>
        /// <param name="carbrandId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstCarBrandAsync(int carbrandId)
        {
            //判断传过来的值是否存在
            if(await dbContext.carBrandInfoRepository.IsExistAsync(carbrandId))
            {
                //找到这条数据
                CarBrandInfo carBrandInfo = await dbContext.carBrandInfoRepository.GetFirstInfo(carbrandId);
                return Ok(carBrandInfo);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 添加车辆品牌信息
        /// </summary>
        /// <param name="carBrandInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertCarBrandAsync(CarBrandInfo carBrandInfo)
        {
            dbContext.carBrandInfoRepository.CreateInfo(carBrandInfo);
            if(await dbContext.carBrandInfoRepository.SaveAsync())
            {
                return Ok(1);
            }
            return Ok("添加失败");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="carbrandId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCarBrandAsync(int carbrandId)
        {
            //判断传过来的值是否存在
            if(await dbContext.carBrandInfoRepository.IsExistAsync(carbrandId))
            {
                //找到这条数据
                CarBrandInfo carBrandInfo = await dbContext.carBrandInfoRepository.GetFirstInfo(carbrandId);
                //删除找到的这条数据
                dbContext.carBrandInfoRepository.DeleteInfo(carBrandInfo);
                if(await dbContext.carBrandInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 修改车辆品牌信息
        /// </summary>
        /// <param name="carBrandInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCarBrandAsync(CarBrandInfo carBrandInfo)
        {
            //判断传过来的值是否存在
            if(await dbContext.carBrandInfoRepository.IsExistAsync(carBrandInfo.CarBrandID))
            {
                //找到这条数据
                CarBrandInfo brandInfo = await dbContext.carBrandInfoRepository.GetFirstInfo(carBrandInfo.CarBrandID);
                //修改数据
                dbContext.carBrandInfoRepository.UpdateInfo(brandInfo);
                if(await dbContext.carBrandInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //如果不存在返回错误信息
            return NotFound();
        }
    }
}
