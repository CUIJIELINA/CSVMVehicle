using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class FaultRecordInfo
    {
        [Key]
        public int FaultID { get; set; }
        public string CarNumber { get; set; }
        public string VDSNumber { get; set; }
        public int CarBrandID { get; set; }
        public string VehicleClass { get; set; }
        public string Project { get; set; }
        public int AbilityID { get; set; }
        public int GroupID { get; set; }
        public string FaultRemark { get; set; }
        public string Keyword { get; set; }
        public string Picture { get; set; }
        public int ComplaintRating { get; set; }
        public string KPMNumber { get; set; }
        public int Mileage { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateGroup { get; set; }
        public string Founder { get; set; }
    }
}
