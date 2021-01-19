using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SAICVolkswagenVehicleManagement_Helper;
using SAICVolkswagenVehicleManagement_Model;
using SAICVolkswagenVehicleManagement.Api.Models;

namespace SAICVolkswagenVehicleManagement.Api.Controllers
{
    /// <summary>
    /// 故障记录信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class FaultRecordController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<FaultRecordController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public FaultRecordController(IRepositoryWrapper dbContext, ILogger<FaultRecordController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// 显示故障信息表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFaultRecordAsync()
        {
            try
            {
                //找到故障记录信息
                IEnumerable<FaultRecordInfo> faultRecordInfos = await dbContext.faultRecordInfoRepository.GetAllInfoAsync();
                //找到车辆品牌信息
                IEnumerable<CarBrandInfo> carBrandInfos = await dbContext.carBrandInfoRepository.GetAllInfoAsync();
                //找到试验类型信息
                IEnumerable<AbilityInfo> abilityInfos = await dbContext.abilityInfoRepository.GetAllInfoAsync();
                //找到组别分类信息
                IEnumerable<GroupInfo> groupInfos = await dbContext.groupInfoRepository.GetAllInfoAsync();
                //四表联查
                var list = (from g in groupInfos.ToList()
                            join f in faultRecordInfos.ToList() on g.GroupID equals f.GroupID
                            join c in carBrandInfos.ToList() on f.CarBrandID equals c.CarBrandID
                            join a in abilityInfos.ToList() on f.AbilityID equals a.DriverAbilityID
                            join zg in groupInfos.ToList() on g.UpID equals zg.GroupID
                            select new FaultrecordAndGroupAndCarbrandAndAbilityDto()
                            {
                                AbilityID = a.DriverAbilityID,
                                DriverAbilityName = a.DriverAbilityName,
                                CarBrandCode = c.CarBrandCode,
                                CarBrandID = c.CarBrandID,
                                CarBrandName = c.CarBrandName,
                                CarNumber = f.CarNumber,
                                ComplaintRating = f.ComplaintRating,
                                CreateDate = f.CreateDate,
                                CreateGroup = f.CreateGroup,
                                FaultID = f.FaultID,
                                FaultRemark = f.FaultRemark,
                                Founder = f.Founder,
                                GroupCode = g.GroupCode,
                                GroupID = g.GroupID,
                                GroupName = g.GroupName,
                                Keyword = f.Keyword,
                                KPMNumber = f.KPMNumber,
                                Mileage = f.Mileage,
                                Picture = f.Picture,
                                Project = f.Project,
                                VDSNumber = f.VDSNumber,
                                VehicleClass = f.VehicleClass,
                                typeName = zg.GroupName,
                                TypeId = zg.GroupID
                            }).ToList();
                //记录日志
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，提示信息：显示故障信息");
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据条件显示一条故障信息
        /// </summary>
        /// <param name="faultrecordId">故障记录ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstFaultRecordAsync(int faultrecordId)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.faultRecordInfoRepository.IsExistAsync(faultrecordId))
                {
                    //找到这条数据
                    FaultRecordInfo faultRecordInfo = await dbContext.faultRecordInfoRepository.GetFirstInfo(faultrecordId);
                    _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},提示信息：根据条件显示一条故障信息");
                    return Ok(faultRecordInfo);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},错误信息：{NotFound()}");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 添加故障记录表
        /// </summary>
        /// <param name="faultRecordInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertFaultRecordAsync(FaultRecordInfo faultRecordInfo)
        {
            try
            {
                dbContext.faultRecordInfoRepository.CreateInfo(faultRecordInfo);
                if (await dbContext.faultRecordInfoRepository.SaveAsync())
                {
                    _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},提示信息：添加故障记录表");
                    return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},错误信息：添加失败");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="faultrecordId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteFaultRecordAsync(int faultrecordId)
        {
            try
            {
                //判断传过来的信息是否存在
                if (await dbContext.faultRecordInfoRepository.IsExistAsync(faultrecordId))
                {
                    //找到这条数据
                    FaultRecordInfo faultRecordInfo = await dbContext.faultRecordInfoRepository.GetFirstInfo(faultrecordId);
                    //删除这条数据
                    dbContext.faultRecordInfoRepository.DeleteInfo(faultRecordInfo);
                    if (await dbContext.faultRecordInfoRepository.SaveAsync())
                    {
                        //记录日志
                        _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},提示信息：删除成功");
                        return Ok(1);
                    }
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},错误信息：{NotFound()}");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 修改一条故障记录数据
        /// </summary>
        /// <param name="faultRecordInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateFaultRecordAsync(FaultRecordInfo faultRecordInfo)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.faultRecordInfoRepository.IsExistAsync(faultRecordInfo.FaultID))
                {
                    //找到这条数据
                    FaultRecordInfo faultRecord = await dbContext.faultRecordInfoRepository.GetFirstInfo(faultRecordInfo.FaultID);
                    //修改其中的数据,并保存数据
                    dbContext.faultRecordInfoRepository.UpdateInfo(faultRecord);
                    if (await dbContext.faultRecordInfoRepository.SaveAsync())
                    {
                        //记录日志
                        _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},提示信息：修改成功");
                        return Ok(1);
                    }
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},错误信息：{NotFound()}");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")},错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
