using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class RoleInfo
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
