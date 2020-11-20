using SAICVolkswagenVehicleManagement_Common.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class CarInfo
    {
        [Key]
        public int CarID { get; set; }
        public CarType CarType { get; set; }
        public string CarCode { get; set; }
        public string CarNumber { get; set; }
        public string CarColor { get; set; }
        public int CarBrandID { get; set; }
        public string CarPicture { get; set; }
        public string PersonLiable { get; set; }
        public int IsUse { get; set; }
        public string CarDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserID { get; set; }
        public int EquipmentID { get; set; }
    }
}
