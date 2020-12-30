using SAICVolkswagenVehicleManagement_Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement.Api.Models
{
    /// <summary>
    /// 驾驶员信息Dto
    /// </summary>
    public class DriverInfoDto
    {
        /// <summary>
        /// 驾驶员Id
        /// </summary>
        public int DriverID { get; set; }
        /// <summary>
        /// 驾驶员工号
        /// </summary>
        public string DriverCode { get; set; }
        /// <summary>
        /// 驾驶员姓名
        /// </summary>
        public string DriverName { get; set; }
        /// <summary>
        /// 驾驶员性别
        /// </summary>
        public int DriverSex { get; set; }
        /// <summary>
        /// 驾照
        /// </summary>
        public string DriverNumber { get; set; }
        /// <summary>
        /// 驾照类型
        /// </summary>
        public DriverType DriverType { get; set; }
        /// <summary>
        /// 驾驶员状态
        /// </summary>
        public DriverState DriverState { get; set; }
        /// <summary>
        /// 车辆Id
        /// </summary>
        public int CarID { get; set; }
        /// <summary>
        /// 是否再用
        /// </summary>
        public int IsState { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 驾照号
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int QuasiDriving { get; set; }
        /// <summary>
        /// 驾驶员描述
        /// </summary>
        public string DriverRemark { get; set; }
        /// <summary>
        /// 班级Id
        /// </summary>
        public int ClassID { get; set; }
        /// <summary>
        /// 品牌Id
        /// </summary>
        public int CarBrandID { get; set; }
        /// <summary>
        /// 车辆编号
        /// </summary>
        public string CarCode { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string CarBrandName { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 驾驶员能力
        /// </summary>
        public string DriverAbilityName { get; set; }
        /// <summary>
        /// 能力Id
        /// </summary>
        public int AbilityID { get; set; }
    }
}
