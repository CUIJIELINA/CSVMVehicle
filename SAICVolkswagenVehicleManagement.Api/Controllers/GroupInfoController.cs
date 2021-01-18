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
    /// 组别信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class GroupInfoController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<GroupInfoController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public GroupInfoController(IRepositoryWrapper dbContext, ILogger<GroupInfoController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }
        /// <summary>
        /// 获取组别信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetGroupAsync()
        {
            try
            {
                //获取组别信息表
                IEnumerable<GroupInfo> groupInfos = await dbContext.groupInfoRepository.GetAllInfoAsync();
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，提示信息：显示组别信息");
                return Ok(groupInfos.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 根据条件找到一条数据
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstGroupAsync(int GroupId)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.groupInfoRepository.IsExistAsync(GroupId))
                {
                    //找到这条数据
                    GroupInfo groupInfo = await dbContext.groupInfoRepository.GetFirstInfo(GroupId);
                    _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，提示信息：显示一条组别");
                    return Ok(groupInfo);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：{NotFound()}");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 添加组别信息
        /// </summary>
        /// <param name="groupInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertGroupInfoAsync(GroupInfo groupInfo)
        {
            try
            {
                dbContext.groupInfoRepository.CreateInfo(groupInfo);
                if (await dbContext.groupInfoRepository.SaveAsync())
                {
                    _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，提示信息：添加组别");
                    return Ok(1);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：添加失败");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteGroupInfoAsync(int GroupId)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.groupInfoRepository.IsExistAsync(GroupId))
                {
                    //找到这一条数据
                    GroupInfo groupInfo = await dbContext.groupInfoRepository.GetFirstInfo(GroupId);
                    //删除数据
                    dbContext.groupInfoRepository.DeleteInfo(groupInfo);
                    if (await dbContext.groupInfoRepository.SaveAsync())
                    {
                        _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，提示信息：删除组别数据");
                        return Ok(1);
                    }
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：{NotFound()}");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 修改组别信息
        /// </summary>
        /// <param name="groupInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateGroupInfoAsync(GroupInfo groupInfo)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.groupInfoRepository.IsExistAsync(groupInfo.GroupID))
                {
                    //找到这条数据
                    GroupInfo group = await dbContext.groupInfoRepository.GetFirstInfo(groupInfo.GroupID);
                    //修改这条数据
                    group.GroupCode = groupInfo.GroupCode;
                    group.GroupName = groupInfo.GroupName;
                    group.UpID = groupInfo.UpID;
                    dbContext.groupInfoRepository.UpdateInfo(group);
                    if (await dbContext.groupInfoRepository.SaveAsync())
                    {
                        _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，提示信息：修改了组别信息");
                        return Ok(1);
                    }
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：{NotFound()}");
                //如果不存在返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}，错误信息：{ex.Message}");
                throw new Exception(ex.Message);
            }

        }
    }
}
