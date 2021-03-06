﻿using System;
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
    /// 驾驶员能力
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class AbilityController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<AbilityController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public AbilityController(IRepositoryWrapper dbContext,ILogger<AbilityController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }
        /// <summary>
        /// 获取能力信息 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAbilityAsync()
        {
            try
            {
                IEnumerable<AbilityInfo> abilityInfos = await dbContext.abilityInfoRepository.GetAllInfoAsync();
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示能力信息");
                return Ok(abilityInfos.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取一条能力信息
        /// </summary>
        /// <param name="abilityId">驾驶员能力Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstAbilityAsync(int abilityId)
        {
            try
            {
                //判断传过来的信息是否存在
                if (await dbContext.abilityInfoRepository.IsExistAsync(abilityId))
                {
                    //找到这条数据
                    AbilityInfo abilityInfo = await dbContext.abilityInfoRepository.GetFirstInfo(abilityId);
                    return Ok(abilityInfo);
                }
                //记录日志
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}获取一条能力信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 添加一条能力信息
        /// </summary>
        /// <param name="abilityInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertAbilityAsync(AbilityInfo abilityInfo)
        {
            try
            {
                dbContext.abilityInfoRepository.CreateInfo(abilityInfo);
                if (await dbContext.abilityInfoRepository.SaveAsync())
                    return Ok(1);
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}添加一条能力信息");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="abilityId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAbilityAsync(int abilityId)
        {
            try
            {
                //判断是否存在这条数据
                if (await dbContext.abilityInfoRepository.IsExistAsync(abilityId))
                {
                    //找到这条数据
                    AbilityInfo abilityInfo = await dbContext.abilityInfoRepository.GetFirstInfo(abilityId);
                    //删除这条数据
                    dbContext.abilityInfoRepository.DeleteInfo(abilityInfo);
                    if (await dbContext.abilityInfoRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}删除一条能力信息");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 修改能力信息
        /// </summary>
        /// <param name="abilityInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAbilityAsync(AbilityInfo abilityInfo)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.abilityInfoRepository.IsExistAsync(abilityInfo.DriverAbilityID))
                {
                    //找到这条数据
                    AbilityInfo ability = await dbContext.abilityInfoRepository.GetFirstInfo(abilityInfo.DriverAbilityID);
                    //修改数据
                    ability.DriverAbilityName = abilityInfo.DriverAbilityName;
                    dbContext.abilityInfoRepository.UpdateInfo(ability);
                    if (await dbContext.abilityInfoRepository.SaveAsync())
                        return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}修改一条能力信息");
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
