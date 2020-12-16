using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement.Api.Models
{
    /// <summary>
    /// 驾驶员和车辆信息
    /// </summary>
    public class DriverAndCarDto
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
        /// 车辆Id
        /// </summary>
        public int CarID { get; set; }
        /// <summary>
        /// 是否使用该车辆
        /// </summary>
        public int IsUse { get; set; }
    }
}
