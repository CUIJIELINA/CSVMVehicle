using SAICVolkswagenVehicleManagement_Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class DriverInfo
    {
        [Key]
        public int DriverID { get; set; }
        public string DriverCode { get; set; }
        public string DriverName { get; set; }
        public int DriverSex { get; set; }
        public string DriverNumber { get; set; }
        public DriverType DriverType { get; set; }
        public DriverState DriverState { get; set; }
        public int CarID { get; set; }
        public int IsState { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Birthday { get; set; }
        public string IDNumber { get; set; }
        public int QuasiDriving { get; set; }
        public string DriverRemark { get; set; }
    }
}
