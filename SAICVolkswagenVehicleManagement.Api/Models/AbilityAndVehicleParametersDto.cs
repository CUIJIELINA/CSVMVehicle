using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement.Api.Models
{
    /// <summary>
    /// 能力和车辆参数信息
    /// </summary>
    public class AbilityAndVehicleParametersDto
    {
        /// <summary>
        /// 车辆参数Id
        /// </summary>
        public int VPId { get; set; }
        /// <summary>
        /// 车号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// VDS号
        /// </summary>
        public string VDSNumber { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectNumber { get; set; }
        /// <summary>
        /// 车型
        /// </summary>
        public string CarModel { get; set; }
        /// <summary>
        /// 能力Id
        /// </summary>
        public int AbilityId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StateDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 发动机结构
        /// </summary>
        public string EngineStructure { get; set; }
        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineNumber { get; set; }
        /// <summary>
        /// 变速箱式样
        /// </summary>
        public string Transmission { get; set; }
        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public string TyreSize { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 当前里程
        /// </summary>
        public double CurrentMileage { get; set; }
        /// <summary>
        /// 剩余里程
        /// </summary>
        public double RemainingMileage { get; set; }
        /// <summary>
        /// 里程表里程
        /// </summary>
        public double Odometer { get; set; }
        /// <summary>
        /// 应跑里程
        /// </summary>
        public double MileageToRun { get; set; }
        /// <summary>
        /// 剩余每周所需班次
        /// </summary>
        public string RemainingFrequency { get; set; }
        /// <summary>
        /// 能力名称
        /// </summary>
        public string DriverAbilityName { get; set; }
    }
}
