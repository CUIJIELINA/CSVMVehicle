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
        public VehicleParametersController(IRepositoryWrapper dbContext,ILogger<VehicleParametersController> logger)
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
                IEnumerable<VehicleParameters> vehicleParameters = await dbContext.vehicleParametersRepository.GetAllInfoAsync();
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示车辆参数信息");
                return Ok(vehicleParameters.ToList());
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
                dbContext.vehicleParametersRepository.CreateInfo(vehicleParameters);
                if (await dbContext.vehicleParametersRepository.SaveAsync())
                    return Ok(1);
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
            //判断传过来的数据是否存在
            if(await dbContext.vehicleParametersRepository.IsExistAsync(vehicleparameterId))
            {
                //找到这条数据
                VehicleParameters vehicleParameters = await dbContext.vehicleParametersRepository.GetFirstInfo(vehicleparameterId);
                //删除数据
                dbContext.vehicleParametersRepository.DeleteInfo(vehicleParameters);
                if(await dbContext.vehicleParametersRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
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
