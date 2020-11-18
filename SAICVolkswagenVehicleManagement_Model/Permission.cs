using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class Permission
    {
        [Key]
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public int UpID { get; set; }
        public string PermissionUrl { get; set; }
    }
}
