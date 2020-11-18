using SAICVolkswagenVehicleManagement_Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class TestSiteInfo
    {
        [Key]
        public int TestSiteID { get; set; }
        [MaxLength(500)]
        public string AddressDetails { get; set; }
        [MaxLength(50)]
        public string Contacts { get; set; }
        [MaxLength(50)]
        public string ContactsPhone { get; set; }
        [MaxLength(50)]
        public string SpotSpeed { get; set; }
        public DateTime CreateDate { get; set; }
        public int IsDelete { get; set; }
        public int AddressState { get; set; }
        public string StartintPoint { get; set; }
        public string TerminationPoint { get; set; }
        public AddressType AddressType { get; set; }
        public string AddressIdentificationCode { get; set; }
    }
}
