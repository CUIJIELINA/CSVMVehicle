using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class Role_Permission
    {
        [Key]
        public int ConnectionID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
        public int Permission { get; set; }
    }
}
