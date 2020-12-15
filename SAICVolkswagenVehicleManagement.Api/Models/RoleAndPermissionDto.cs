using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAICVolkswagenVehicleManagement.Api.Models
{
    /// <summary>
    /// 角色和权限同时添加
    /// </summary>
    public class RoleAndPermissionDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Role_Name { get; set; }
        /// <summary>
        /// 添加的权限字符串Id
        /// </summary>
        public string PermissionIds { get; set; }
    }
}
