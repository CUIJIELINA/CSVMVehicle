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
        /// <summary>
        /// 1真删除 0假删除 
        /// </summary>
        public int IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
