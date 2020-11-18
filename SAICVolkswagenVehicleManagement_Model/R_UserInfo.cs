using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class R_UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [MaxLength(50)]
        public string UserCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserPassWord { get; set; }
        public int Department_ID { get; set; }
        public string UserSex { get; set; }
        public string UserRemark { get; set; }
        public string E_Mail { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
