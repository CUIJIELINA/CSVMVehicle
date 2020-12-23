using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SAICVolkswagenVehicleManagement.Api.Models;
using SAICVolkswagenVehicleManagement_Helper;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement.Api.Controllers
{
    /// <summary>
    /// 车辆参数表
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class VehicleParametersController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<VehicleParametersController> _logger;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public VehicleParametersController(IRepositoryWrapper dbContext, ILogger<VehicleParametersController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 显示车辆参数信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetVehicleParametersAsync()
        {
            try
            {
                //找到车辆参数信息
                IEnumerable<VehicleParameters> vehicleParameters = await dbContext.vehicleParametersRepository.GetAllInfoAsync();
                //找到能力信息
                IEnumerable<AbilityInfo> abilityInfos = await dbContext.abilityInfoRepository.GetAllInfoAsync();
                //两表联查
                List<AbilityAndVehicleParametersDto> list = (from a in abilityInfos.ToList()
                                                             join v in vehicleParameters.ToList() on a.DriverAbilityID equals v.AbilityId
                                                             select new AbilityAndVehicleParametersDto()
                                                             {
                                                                 AbilityId = a.DriverAbilityID,
                                                                 CarModel = v.CarModel,
                                                                 CarNumber = v.CarNumber,
                                                                 CurrentMileage = v.CurrentMileage,
                                                                 DriverAbilityName = a.DriverAbilityName,
                                                                 EndDate = v.EndDate,
                                                                 EngineNumber = v.EngineNumber,
                                                                 EngineStructure = v.EngineStructure,
                                                                 MileageToRun = v.MileageToRun,
                                                                 Odometer = v.Odometer,
                                                                 ProjectNumber = v.ProjectNumber,
                                                                 RemainingFrequency = v.RemainingFrequency,
                                                                 RemainingMileage = v.RemainingMileage,
                                                                 Remark = v.Remark,
                                                                 StateDate = v.StateDate,
                                                                 Transmission = v.Transmission,
                                                                 TyreSize = v.TyreSize,
                                                                 VDSNumber = v.VDSNumber,
                                                                 VPId = v.VPId
                                                             }).ToList();
                //计算一些数据（里程和班次），循环List里面的所有数据
                foreach (var item in list)
                {
                    //如果里程表里有里程
                    if(item.Odometer > 0)
                        item.CurrentMileage = item.MileageToRun - item.Odometer;
                    //剩余里程
                    item.RemainingMileage = item.MileageToRun - item.CurrentMileage;
                    //剩余每周所需班次
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示车辆参数信息");
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 显示一条车辆参数信息
        /// </summary>
        /// <param name="vehicleparameterId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstVehicleParameterAsync(int vehicleparameterId)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.vehicleParametersRepository.IsExistAsync(vehicleparameterId))
                {
                    //找到这条数据
                    VehicleParameters vehicleParameters = await dbContext.vehicleParametersRepository.GetFirstInfo(vehicleparameterId);
                    _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示一条车辆参数信息");
                    return Ok(vehicleParameters);
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
        /// 添加车辆参数信息
        /// </summary>
        /// <param name="vehicleParameters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertVehicleParametersAsync(VehicleParameters vehicleParameters)
        {
            try
            {
                //自动计算当前里程和剩余里程，在前台计算，直接赋值，不可编辑
                dbContext.vehicleParametersRepository.CreateInfo(vehicleParameters);
                if (await dbContext.vehicleParametersRepository.SaveAsync())
                    return Ok(1);
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}添加车辆参数信息");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除一条信息
        /// </summary>
        /// <param name="vehicleparameterId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteVehicleParametersAsync(int vehicleparameterId)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.vehicleParametersRepository.IsExistAsync(vehicleparameterId))
                {
                    //找到这条数据
                    VehicleParameters vehicleParameters = await dbContext.vehicleParametersRepository.GetFirstInfo(vehicleparameterId);
                    //删除数据
                    dbContext.vehicleParametersRepository.DeleteInfo(vehicleParameters);
                    if (await dbContext.vehicleParametersRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}删除一条车辆参数信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="vehicleParameters"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateVehicleParametersAsync(VehicleParameters vehicleParameters)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.vehicleParametersRepository.IsExistAsync(vehicleParameters.VPId))
                {
                    //找到这条数据
                    VehicleParameters vehicle = await dbContext.vehicleParametersRepository.GetFirstInfo(vehicleParameters.VPId);
                    //修改数据
                    dbContext.vehicleParametersRepository.UpdateInfo(vehicle);
                    if (await dbContext.vehicleParametersRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}修改一条车辆参数信息");
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
