using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class User_Role
    {
        [Key]
        public int ConnectionID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
}
