using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class TestSiteAndCar
    {
        [Key]
        public int TestSiteAndCarID { get; set; }
        public int CarID { get; set; }
        public int TestSiteID { get; set; }
    }
}
