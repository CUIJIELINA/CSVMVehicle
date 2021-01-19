using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement.Api.Models
{
    /// <summary>
    /// 故障记录信息表Dto
    /// </summary>
    public class FaultrecordAndGroupAndCarbrandAndAbilityDto
    {
        /// <summary>
        /// 故障ID
        /// </summary>
        public int FaultID { get; set; }
        /// <summary>
        /// 车号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// VDS号
        /// </summary>
        public string VDSNumber { get; set; }
        /// <summary>
        /// 车辆品牌ID
        /// </summary>
        public int CarBrandID { get; set; }
        /// <summary>
        /// 车辆等级
        /// </summary>
        public string VehicleClass { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Project { get; set; }
        /// <summary>
        /// 实验类型ID
        /// </summary>
        public int AbilityID { get; set; }
        /// <summary>
        /// 组别分类ID
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        public string FaultRemark { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; set; }
        /// <summary>
        /// 抱怨评级
        /// </summary>
        public int ComplaintRating { get; set; }
        /// <summary>
        /// KPM编号
        /// </summary>
        public string KPMNumber { get; set; }
        /// <summary>
        /// 里程
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 创建组
        /// </summary>
        public string CreateGroup { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Founder { get; set; }
        /// <summary>
        /// 组别编号
        /// </summary>
        public string GroupCode { get; set; }
        /// <summary>
        /// 组别名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 车辆品牌编号
        /// </summary>
        public string CarBrandCode { get; set; }
        /// <summary>
        /// 车辆品牌名称
        /// </summary>
        public string CarBrandName { get; set; }
        /// <summary>
        /// 试验类型名称
        /// </summary>
        public string DriverAbilityName { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string typeName { get; set; }
        public int TypeId { get; set; }
    }
}
