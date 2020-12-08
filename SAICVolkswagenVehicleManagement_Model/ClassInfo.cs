using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class ClassInfo
    {
        [Key]
        public int ClassID { get; set; }
        public string ClassName { get; set; }
    }
}
