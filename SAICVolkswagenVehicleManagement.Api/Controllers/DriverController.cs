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
        private readonly string filePath = @"D:\Document\上汽大众\试车员试验能力档案及提升目标2.0(1).xlsx";
        //上下文
        private readonly IRepositoryWrapper dbContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext"></param>
        public DriverController(IRepositoryWrapper dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// 获取驾驶员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  async Task<IActionResult> GetDriverInfoAsync()
        {
            IEnumerable<DriverInfo> driverInfos = await dbContext.driverInfoRepository.GetAllInfoAsync();
            return Ok(driverInfos.ToList());
        }

        /// <summary>
        /// 找到一条驾驶员信息
        /// </summary>
        /// <param name="driverId">驾驶员Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFirstDriver(int driverId)
        {
            //判断传过来的值是否存在
            if(await dbContext.driverInfoRepository.IsExistAsync(driverId))
            {
                //找到这一条数据
                DriverInfo driverInfo = await dbContext.driverInfoRepository.GetFirstInfo(driverId);
                return Ok(driverInfo);
            }
            //如果没有找到，返回错误信息
            return NotFound();
        }

        /// <summary>
        /// 添加驾驶员
        /// </summary>
        /// <param name="driverInfo">驾驶员信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InsertDriverAsync(DriverInfo driverInfo)
        {
            dbContext.driverInfoRepository.CreateInfo(driverInfo);
            if(await dbContext.driverInfoRepository.SaveAsync())
            {
                return Ok(1);
            }
            return Ok("添加失败");
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
            if(await dbContext.driverInfoRepository.IsExistAsync(driverId))
            {
                //如果存在找到这条数据
                DriverInfo driverInfo = await dbContext.driverInfoRepository.GetFirstInfo(driverId);
                //删除数据
                dbContext.driverInfoRepository.DeleteInfo(driverInfo);
                if(await dbContext.driverInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
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
            if(await dbContext.driverInfoRepository.IsExistAsync(driverInfo.DriverID))
            {
                //找到这条数据
                DriverInfo driver = await dbContext.driverInfoRepository.GetFirstInfo(driverInfo.DriverID);
                driver.DriverName = driverInfo.DriverName;
                dbContext.driverInfoRepository.UpdateInfo(driver);
                if(await dbContext.driverInfoRepository.SaveAsync())
                {
                    return Ok(1);
                }
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
            Dictionary<int, object> pairs = new Dictionary<int, object>();
            Workbook workbooks = new Workbook();
            workbooks.LoadFromFile(filePath);
            Worksheet worksheets = workbooks.Worksheets[0];
            CellRange ranges = worksheets.Range["Item"];
            CellRange range = worksheets.Range["TailTitle"];
            for (int i = ranges.First().Row; i < range.First().Row; i++)
            {
                if (ranges[i, ranges.First().Column].Text == null)
                    continue;
                pairs.Add(i,ranges[i,ranges.First().Column].Text);
            }
            int res = 0;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("insert into DriverInfo(DriverName) values ");
            using (IDbConnection conn = new SqlConnection { ConnectionString = connectionString })
            {
                foreach (var item in pairs)
                    stringBuilder.Append($"('{item.Value}'),");
                string sql = stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
                res = conn.Execute(sql);
            }
            if(res > 0)
                return Ok(1);
            return Ok("导入失败");
        }
    }
}
