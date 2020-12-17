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
    /// 车辆信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<CarController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public CarController(IRepositoryWrapper dbContext,ILogger<CarController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 获取车辆信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCarAsync()
        {
            try
            {
                //获取车辆信息
                IEnumerable<CarInfo> carInfos = await dbContext.carInfoRepository.GetAllInfoAsync();
                //获取车辆品牌信息
                IEnumerable<CarBrandInfo> carBrandInfos = await dbContext.carBrandInfoRepository.GetAllInfoAsync();
                //两表联查
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示车辆信息");
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据Id获取对应的一条信息
        /// </summary>
        /// <param name="carId">车辆Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstCarAsync(int carId)
        {
            try
            {
                //判断传过来的Id是否存在
                if (await dbContext.carInfoRepository.IsExistAsync(carId))
                {
                    //找到这条信息
                    CarInfo carInfo = await dbContext.carInfoRepository.GetFirstInfo(carId);
                    return Ok(carInfo);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示一条车辆信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除车辆信息
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCarAsync(int carId)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.carInfoRepository.IsExistAsync(carId))
                {
                    //找到这条数据
                    CarInfo carInfo = await dbContext.carInfoRepository.GetFirstInfo(carId);
                    //删除数据
                    dbContext.carInfoRepository.DeleteInfo(carInfo);
                    if (await dbContext.carInfoRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}删除车辆信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 修改车辆信息
        /// </summary>
        /// <param name="carInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCarAsync(CarInfo carInfo)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.carInfoRepository.IsExistAsync(carInfo.CarID))
                {
                    //找到这条数据
                    CarInfo info = await dbContext.carInfoRepository.GetFirstInfo(carInfo.CarID);
                    //修改数据
                    dbContext.carInfoRepository.UpdateInfo(info);
                    if (await dbContext.carInfoRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}修改车辆信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 添加车辆信息
        /// </summary>
        /// <param name="carInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertCarAsync(CarInfo carInfo)
        {
            try
            {
                dbContext.carInfoRepository.CreateInfo(carInfo);
                if (await dbContext.carInfoRepository.SaveAsync())
                    return Ok(1);
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}添加车辆信息");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
