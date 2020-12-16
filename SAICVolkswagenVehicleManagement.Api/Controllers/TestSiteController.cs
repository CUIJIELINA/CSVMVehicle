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
    /// 试验地点信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestSiteController : ControllerBase
    {
        private readonly IRepositoryWrapper dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public TestSiteController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 获取试验地点信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTestSiteAsync()
        {
            try
            {
                IEnumerable<TestSiteInfo> testSiteInfos = await dbContext.testSiteInfoRepository.GetAllInfoAsync();
                return Ok(testSiteInfos.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 获取试验地点的一条信息
        /// </summary>
        /// <param name="TestSiteId">试验地点ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstTestSiteAsync(int TestSiteId)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.testSiteInfoRepository.IsExistAsync(TestSiteId))
                {
                    //找到这条数据
                    TestSiteInfo testSiteInfo = await dbContext.testSiteInfoRepository.GetFirstInfo(TestSiteId);
                    return Ok(testSiteInfo);
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
        /// 添加试验地点信息
        /// </summary>
        /// <param name="testSiteInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertTestSiteAsync(TestSiteInfo testSiteInfo)
        {
            try
            {
                dbContext.testSiteInfoRepository.CreateInfo(testSiteInfo);
                if (await dbContext.testSiteInfoRepository.SaveAsync())
                    return Ok(1);
                return Ok("注册失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="TestSiteId">试验地点ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteTestSiteAsync(int TestSiteId)
        {
            try
            {
                //判断传过来的数据是否存在
                if (await dbContext.testSiteInfoRepository.IsExistAsync(TestSiteId))
                {
                    //找到这条数据
                    TestSiteInfo testSiteInfo = await dbContext.testSiteInfoRepository.GetFirstInfo(TestSiteId);
                    //删除数据
                    dbContext.testSiteInfoRepository.DeleteInfo(testSiteInfo);
                    if (await dbContext.testSiteInfoRepository.SaveAsync())
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
        /// 修改试验地点数据
        /// </summary>
        /// <param name="testSiteInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTestSiteAsync(TestSiteInfo testSiteInfo)
        {
            try
            {
                //判断传过来的信息是否存在
                if (await dbContext.testSiteInfoRepository.IsExistAsync(testSiteInfo.TestSiteID))
                {
                    //找到这条数据
                    TestSiteInfo testSite = await dbContext.testSiteInfoRepository.GetFirstInfo(testSiteInfo.TestSiteID);
                    //修改数据
                    dbContext.testSiteInfoRepository.UpdateInfo(testSite);
                    if (await dbContext.testSiteInfoRepository.SaveAsync())
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