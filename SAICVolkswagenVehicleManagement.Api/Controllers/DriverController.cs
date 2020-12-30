using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using SAICVolkswagenVehicleManagement_Helper;
using SAICVolkswagenVehicleManagement_Model;
using Spire.Xls;
using CellRange = Spire.Xls.CellRange;
using Dapper;
using SAICVolkswagenVehicleManagement.Api.Models;
using Microsoft.Extensions.Logging;

namespace SAICVolkswagenVehicleManagement.Api.Controllers
{

    /// <summary>
    /// 驾驶员信息
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        //连接数据库
        private readonly string connectionString = "Data Source=192.168.1.6;Initial Catalog=CSVMVehicle;User ID=sa;PassWord=123456";
        //导入的Excel文档
        private readonly string filePath = @"D:\Document\上汽大众\试车员试验能力档案及提升目标2.0(2).xlsx";
        //上下文
        private readonly IRepositoryWrapper dbContext;
        private readonly ILogger<DriverController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public DriverController(IRepositoryWrapper dbContext, ILogger<DriverController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }
        /// <summary>
        /// 获取驾驶员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDriverInfoAsync()
        {
            try
            {
                //驾驶员信息
                IEnumerable<DriverInfo> driverInfos = await dbContext.driverInfoRepository.GetAllInfoAsync();
                //车辆信息
                IEnumerable<CarInfo> carInfos = await dbContext.carInfoRepository.GetAllInfoAsync();
                //品牌信息
                IEnumerable<CarBrandInfo> carBrandInfos = await dbContext.carBrandInfoRepository.GetAllInfoAsync();
                //班级信息
                IEnumerable<ClassInfo> classInfos = await dbContext.classInfoRepository.GetAllInfoAsync();
                //获取能力信息
                IEnumerable<AbilityInfo> abilityInfos = await dbContext.abilityInfoRepository.GetAllInfoAsync();
                //五表联查
                List<DriverInfoDto> list = (from d in driverInfos.ToList()
                                            join c in carInfos.ToList() on d.CarID equals c.CarID
                                            join cb in carBrandInfos.ToList() on c.CarBrandID equals cb.CarBrandID
                                            join cl in classInfos.ToList() on d.ClassID equals cl.ClassID
                                            join a in abilityInfos.ToList() on d.AbilityID equals a.DriverAbilityID
                                            select new DriverInfoDto()
                                            {
                                                CarBrandID = cb.CarBrandID,
                                                CarBrandName = cb.CarBrandName,
                                                CarCode = c.CarCode,
                                                CarID = c.CarID,
                                                ClassID = d.ClassID,
                                                DriverCode = d.DriverCode,
                                                DriverID = d.DriverID,
                                                DriverName = d.DriverName,
                                                DriverNumber = d.DriverNumber,
                                                DriverRemark = d.DriverRemark,
                                                DriverSex = d.DriverSex,
                                                DriverState = d.DriverState,
                                                DriverType = d.DriverType,
                                                IDNumber = d.IDNumber,
                                                IsState = d.IsState,
                                                ClassName = cl.ClassName,
                                                AbilityID = a.DriverAbilityID,
                                                DriverAbilityName = a.DriverAbilityName
                                            }).ToList();
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示驾驶员信息");
                return Ok(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 找到一条驾驶员信息
        /// </summary>
        /// <param name="driverId">驾驶员Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstDriver(int driverId)
        {
            try
            {
                //判断传过来的值是否存在
                if (await dbContext.driverInfoRepository.IsExistAsync(driverId))
                {
                    //找到这一条数据
                    DriverInfo driverInfo = await dbContext.driverInfoRepository.GetFirstInfo(driverId);
                    return Ok(driverInfo);
                }
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}显示一条驾驶员信息");
                //如果没有找到，返回错误信息
                return NotFound();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 添加驾驶员
        /// </summary>
        /// <param name="driverInfo">驾驶员信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertDriverAsync(DriverInfo driverInfo)
        {
            try
            {
                dbContext.driverInfoRepository.CreateInfo(driverInfo);
                if (await dbContext.driverInfoRepository.SaveAsync())
                    return Ok(1);
                _logger.LogInformation($"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}添加驾驶员信息");
                return Ok("添加失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 删除对应的一条驾驶员信息
        /// </summary>
        /// <param name="driverId">驾驶员Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteDriverAsync(int driverId)
        {
            //判断传过来的值是否存在
            if (await dbContext.driverInfoRepository.IsExistAsync(driverId))
            {
                //如果存在找到这条数据
                DriverInfo driverInfo = await dbContext.driverInfoRepository.GetFirstInfo(driverId);
                //删除数据
                dbContext.driverInfoRepository.DeleteInfo(driverInfo);
                if (await dbContext.driverInfoRepository.SaveAsync())
                    return Ok(1);
            }
            //如果不存在，返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 修改一条驾驶员信息
        /// </summary>
        /// <param name="driverInfo"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateDriverAsync(DriverInfo driverInfo)
        {
            //判断传过来的值是否存在
            if (await dbContext.driverInfoRepository.IsExistAsync(driverInfo.DriverID))
            {
                //找到这条数据
                DriverInfo driver = await dbContext.driverInfoRepository.GetFirstInfo(driverInfo.DriverID);
                driver.DriverName = driverInfo.DriverName;
                dbContext.driverInfoRepository.UpdateInfo(driver);
                if (await dbContext.driverInfoRepository.SaveAsync())
                    return Ok(1);
            }
            //如果没有找到返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 从Excel表格中导入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ImPortDriverAsync()
        {
            try
            {
                //定义键值对
                Dictionary<int, object> pairs = new Dictionary<int, object>();
                //定义Workbooks
                Workbook workbooks = new Workbook();
                //读取文件
                workbooks.LoadFromFile(filePath);
                int place = 1;
                int sheetCount = workbooks.Worksheets.Count;
                //循环工作薄
                for (int i = 1; i < sheetCount; i++)
                {
                    Worksheet worksheets = workbooks.Worksheets[i];
                    CellRange ranges = worksheets.Range[$"Item{i}"];
                    CellRange range = worksheets.Range[$"TailTitle{i}"];
                    for (int j = ranges.First().Row; j < range.First().Row; j++)
                    {
                        ranges[j, ranges.First().Column].CollapseGroup(GroupByType.ByRows);
                        if (ranges[j, ranges.First().Column].Text == null)
                            continue;
                        pairs.Add(place, ranges[j, ranges.First().Column].Text);
                        place++;
                    }
                }
                int result = 0;
                StringBuilder stringBuilder = new StringBuilder();
                //拼接
                stringBuilder.Append("insert into DriverInfo(DriverName) values ");
                using (IDbConnection connection = new SqlConnection { ConnectionString = connectionString })
                {
                    foreach (var item in pairs)
                    {
                        stringBuilder.Append($"('{item.Value}'),");
                    }
                    string sql = stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
                    result = connection.Execute(sql);
                }
                if (result > 0)
                    return Ok(1);
                return Ok("导入失败");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 驾驶员排班
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DriverSchedulingAsync(DriverAndCarDto model)
        {
            try
            {
                //获取驾驶员信息
                IEnumerable<DriverInfo> driverInfos = await dbContext.driverInfoRepository.GetAllInfoAsync();
                //获取车辆信息
                IEnumerable<CarInfo> carInfos = await dbContext.carInfoRepository.GetAllInfoAsync();
                //两表联查
                var list = from d in driverInfos.ToList()
                           join c in carInfos.ToList() on d.CarID equals c.CarID
                           select new DriverAndCarDto()
                           {
                               CarID = c.CarID,
                               DriverCode = d.DriverCode,
                               DriverID = d.DriverID,
                               DriverName = d.DriverName,
                               IsUse = c.IsUse
                           };
                //根据传过来的值判断这条信息是否存在
                DriverInfo driver = driverInfos.ToList().Where(s => s.DriverID.Equals(model.DriverID)).FirstOrDefault();
                CarInfo car = carInfos.ToList().Where(s => s.CarID.Equals(model.CarID)).FirstOrDefault();
                if (driver == null)
                    throw new Exception("找不到驾驶员数据");
                if (car == null)
                    throw new Exception("找不到车辆数据");
                //驾驶员和车辆随机匹配
                //根据车辆是否被使用查找出这条数据,这是一辆空闲车辆
                var IsUseCar = carInfos.ToList().Where(s => s.IsUse.Equals(0)).FirstOrDefault();
                //找到空闲的驾驶员
                var IsStateDriver = driverInfos.ToList().Where(s => s.IsState.Equals(0)).FirstOrDefault();
                //开始匹配车辆

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
