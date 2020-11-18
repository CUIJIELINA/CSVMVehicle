using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class EquipmentInfo
    {
        [Key]
        public int EquipmentID { get; set; }
        public string EquipmentCode { get; set; }
        public string EquipmentName { get; set; }
        public int EquipmentState { get; set; }
        public string EquipmentRemark { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
