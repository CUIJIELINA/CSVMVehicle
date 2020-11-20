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
    /// 设备信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public EquipmentController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEquipmentAsync()
        {
            IEnumerable<EquipmentInfo> equipmentInfos = await dbContext.equipmentInfoRepository.GetAllInfoAsync();
            return Ok(equipmentInfos.ToList());
        }

        /// <summary>
        /// 添加设备信息
        /// </summary>
        /// <param name="equipmentInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertEquipmentAsync(EquipmentInfo equipmentInfo)
        {
            dbContext.equipmentInfoRepository.CreateInfo(equipmentInfo);
            if(await dbContext.equipmentInfoRepository.SaveAsync())
            {
                return Ok(1);
            }
            return Ok("添加失败");
        }

        /// <summary>
        /// 根据Id获取一条设备信息
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstEquipmentAsync(int equipmentId)
        {
            //判断传过来的值是否存在
            if(await dbContext.equipmentInfoRepository.IsExistAsync(equipmentId))
            {
                //找到一条数据
                EquipmentInfo equipmentInfo = await dbContext.equipmentInfoRepository.GetFirstInfo(equipmentId);
                return Ok(equipmentInfo);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 删除设备数据
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteEquipmentAsync(int equipmentId)
        {
            //判断是否存在
            if(await dbContext.equipmentInfoRepository.IsExistAsync(equipmentId))
            {
                //找到这条数据
                EquipmentInfo equipmentInfo = await dbContext.equipmentInfoRepository.GetFirstInfo(equipmentId);
                //删除数据
                dbContext.equipmentInfoRepository.DeleteInfo(equipmentInfo);
                if(await dbContext.equipmentInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 修改设备信息
        /// </summary>
        /// <param name="equipmentInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEquipmentAsync(EquipmentInfo equipmentInfo)
        {
            //判断信息是否存在
            if(await dbContext.equipmentInfoRepository.IsExistAsync(equipmentInfo.EquipmentID))
            {
                //找到数据
                EquipmentInfo equipment = await dbContext.equipmentInfoRepository.GetFirstInfo(equipmentInfo.EquipmentID);
                //修改数据
                dbContext.equipmentInfoRepository.UpdateInfo(equipment);
                if(await dbContext.equipmentInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
            }
            //如果不存在返回错误信息
            return NotFound();
        }
    }
}
