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
            List<EquipmentInfo> list = equipmentInfos.Where(s => s.IsDelete == 1).ToList();
            return Ok(list);
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
            if (await dbContext.equipmentInfoRepository.SaveAsync())
                return Ok(1);
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
            if (await dbContext.equipmentInfoRepository.IsExistAsync(equipmentId))
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
            if (await dbContext.equipmentInfoRepository.IsExistAsync(equipmentId))
            {
                //找到这条数据
                EquipmentInfo equipmentInfo = await dbContext.equipmentInfoRepository.GetFirstInfo(equipmentId);
                //删除数据
                dbContext.equipmentInfoRepository.DeleteInfo(equipmentInfo);
                if (await dbContext.equipmentInfoRepository.SaveAsync())
                    return Ok(1);
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
            if (await dbContext.equipmentInfoRepository.IsExistAsync(equipmentInfo.EquipmentID))
            {
                //找到数据
                EquipmentInfo equipment = await dbContext.equipmentInfoRepository.GetFirstInfo(equipmentInfo.EquipmentID);
                //修改其中的字段
                equipment.EquipmentName = equipmentInfo.EquipmentName;
                equipment.EquipmentCode = equipmentInfo.EquipmentCode;
                //修改数据
                dbContext.equipmentInfoRepository.UpdateInfo(equipment);
                if (await dbContext.equipmentInfoRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 分配设备信息状态
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> DistributionEquipmentAsync(int equipmentId)
        {
            //判断传过来的数据是否存在
            if (await dbContext.equipmentInfoRepository.IsExistAsync(equipmentId))
            {
                //找到这条数据
                EquipmentInfo equipment = await dbContext.equipmentInfoRepository.GetFirstInfo(equipmentId);
                //使用三目运算符修改数据
                equipment.EquipmentState = equipment.EquipmentState != 1 ? equipment.EquipmentState += 1 : equipment.EquipmentState -= 1;
                //执行修改数据
                dbContext.equipmentInfoRepository.UpdateInfo(equipment);
                //判断是否修改成功，修改成功返回1
                if (await dbContext.equipmentInfoRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="equipmentId">设备ID</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> FalseEquipmentDelete(int equipmentId)
        {
            //判断传过来的数据是否存在
            if (await dbContext.equipmentInfoRepository.IsExistAsync(equipmentId))
            {
                //找到这条数据
                EquipmentInfo equipment = await dbContext.equipmentInfoRepository.GetFirstInfo(equipmentId);
                //使用三目运算符修改数据
                equipment.IsDelete = equipment.IsDelete != 1 ? equipment.IsDelete += 1 : equipment.IsDelete -= 1;
                //执行修改数据
                dbContext.equipmentInfoRepository.UpdateInfo(equipment);
                //判断是都修改成功，修改成功返回1
                if (await dbContext.equipmentInfoRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在返回错误信息
            return NotFound();
        }
    }
}