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
    /// 车辆品牌信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class CarBrandController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<CarBrandController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public CarBrandController(IRepositoryWrapper dbContext,ILogger<CarBrandController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 获取车辆品牌信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCarBrandAsync()
        {
            try
            {
                IEnumerable<CarBrandInfo> carBrandInfos = await dbContext.carBrandInfoRepository.GetAllInfoAsync();
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示车辆品牌信息");
                return Ok(carBrandInfos.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据Id获取一条信息
        /// </summary>
        /// <param name="carbrandId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstCarBrandAsync(int carbrandId)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.carBrandInfoRepository.IsExistAsync(carbrandId))
                {
                    //找到这条数据
                    CarBrandInfo carBrandInfo = await dbContext.carBrandInfoRepository.GetFirstInfo(carbrandId);
                    return Ok(carBrandInfo);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}获取一条车辆品牌信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 添加车辆品牌信息
        /// </summary>
        /// <param name="carBrandInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertCarBrandAsync(CarBrandInfo carBrandInfo)
        {
            try
            {
                dbContext.carBrandInfoRepository.CreateInfo(carBrandInfo);
                if (await dbContext.carBrandInfoRepository.SaveAsync())
                    return Ok(1);
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="carbrandId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCarBrandAsync(int carbrandId)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.carBrandInfoRepository.IsExistAsync(carbrandId))
                {
                    //找到这条数据
                    CarBrandInfo carBrandInfo = await dbContext.carBrandInfoRepository.GetFirstInfo(carbrandId);
                    //删除找到的这条数据
                    dbContext.carBrandInfoRepository.DeleteInfo(carBrandInfo);
                    if (await dbContext.carBrandInfoRepository.SaveAsync())
                        return Ok(1);
                }
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 修改车辆品牌信息
        /// </summary>
        /// <param name="carBrandInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCarBrandAsync(CarBrandInfo carBrandInfo)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.carBrandInfoRepository.IsExistAsync(carBrandInfo.CarBrandID))
                {
                    //找到这条数据
                    CarBrandInfo brandInfo = await dbContext.carBrandInfoRepository.GetFirstInfo(carBrandInfo.CarBrandID);
                    //修改数据
                    brandInfo.CarBrandCode = carBrandInfo.CarBrandCode;
                    brandInfo.CarBrandName = carBrandInfo.CarBrandName;
                    brandInfo.CarBrandDescription = carBrandInfo.CarBrandDescription;
                    brandInfo.CreateDate = DateTime.Now;
                    dbContext.carBrandInfoRepository.UpdateInfo(brandInfo);
                    if (await dbContext.carBrandInfoRepository.SaveAsync())
                        return Ok(1);
                }
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
