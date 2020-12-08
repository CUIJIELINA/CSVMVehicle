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
    /// 驾驶员能力
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class AbilityController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public AbilityController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// 获取能力信息 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAbilityAsync()
        {
            IEnumerable<AbilityInfo> abilityInfos = await dbContext.abilityInfoRepository.GetAllInfoAsync();
            return Ok(abilityInfos.ToList());
        }

        /// <summary>
        /// 获取一条能力信息
        /// </summary>
        /// <param name="abilityId">驾驶员能力Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstAbilityAsync(int abilityId)
        {
            //判断传过来的信息是否存在
            if(await dbContext.abilityInfoRepository.IsExistAsync(abilityId))
            {
                //找到这条数据
                AbilityInfo abilityInfo = await dbContext.abilityInfoRepository.GetFirstInfo(abilityId);
                return Ok(abilityInfo);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 添加一条能力信息
        /// </summary>
        /// <param name="abilityInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertAbilityAsync(AbilityInfo abilityInfo)
        {
            dbContext.abilityInfoRepository.CreateInfo(abilityInfo);
            if(await dbContext.abilityInfoRepository.SaveAsync())
                return Ok(1);
            return Ok("添加失败");
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="abilityId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAbilityAsync(int abilityId)
        {
            //判断是否存在这条数据
            if(await dbContext.abilityInfoRepository.IsExistAsync(abilityId))
            {
                //找到这条数据
                AbilityInfo abilityInfo = await dbContext.abilityInfoRepository.GetFirstInfo(abilityId);
                //删除这条数据
                dbContext.abilityInfoRepository.DeleteInfo(abilityInfo);
                if(await dbContext.abilityInfoRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 修改能力信息
        /// </summary>
        /// <param name="abilityInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAbilityAsync(AbilityInfo abilityInfo)
        {
            //判断传过来的数据是否存在
            if(await dbContext.abilityInfoRepository.IsExistAsync(abilityInfo.DriverAbilityID))
            {
                //找到这条数据
                AbilityInfo ability = await dbContext.abilityInfoRepository.GetFirstInfo(abilityInfo.DriverAbilityID);
                //修改数据
                ability.DriverAbilityName = abilityInfo.DriverAbilityName;
                dbContext.abilityInfoRepository.UpdateInfo(ability);
                if(await dbContext.abilityInfoRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }
    }
}
