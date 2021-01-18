using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class GroupInfo
    {
        [Key]
        public int GroupID { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int UpID { get; set; }
    }
}
