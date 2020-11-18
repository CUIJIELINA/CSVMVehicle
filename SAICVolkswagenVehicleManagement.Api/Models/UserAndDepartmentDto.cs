using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement.Api.Models
{
    /// <summary>
    /// 注册员工信息类
    /// </summary>
    public class UserAndDepartmentDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
       /// <summary>
       /// 用户编号
       /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassWord { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int Department_ID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string UserSex { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string UserRemark { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string E_Mail { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
