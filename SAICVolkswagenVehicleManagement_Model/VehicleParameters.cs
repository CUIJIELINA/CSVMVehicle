using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class VehicleParameters
    {
        [Key]
        public int VPId { get; set; }
        public string CarNumber { get; set; }
        public string VDSNumber { get; set; }
        public string ProjectNumber { get; set; }
        public string CarModel { get; set; }
        public int AbilityId { get; set; }
        public DateTime StateDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EngineStructure { get; set; }
        public string EngineNumber { get; set; }
        public string Transmission { get; set; }
        public string TyreSize { get; set; }
        public string Remark { get; set; }
        public double CurrentMileage { get; set; }
        public double RemainingMileage { get; set; }
        public double Odometer { get; set; }
        public double MileageToRun { get; set; }
        public string RemainingFrequency { get; set; }
    }
}
