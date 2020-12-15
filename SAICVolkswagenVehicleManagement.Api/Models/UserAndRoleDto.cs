using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement.Api.Models
{
    /// <summary>
    /// 用户和角色关联表
    /// </summary>
    public class UserAndRoleDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string User_Name { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
    }
}
