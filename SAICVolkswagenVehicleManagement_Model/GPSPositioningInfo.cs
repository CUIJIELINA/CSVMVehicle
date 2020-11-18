using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class GPSPositioningInfo
    {
        [Key]
        public int GPSID { get; set; }
        public int CarID { get; set; }
        public string GPSCode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int GPSState { get; set; }
        public DateTime LocationDate { get; set; }
        public DateTime RecordDate { get; set; }
        public double Angle { get; set; }
        public double CurrentMileage { get; set; }
        public double TotalMileage { get; set; }
        public string GPSDescription { get; set; }
        public string Level { get; set; }
        public string GPSSerialNumber { get; set; }
    }
}
