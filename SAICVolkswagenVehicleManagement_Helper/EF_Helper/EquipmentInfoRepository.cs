using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class EquipmentInfoRepository : RepositoryBase<EquipmentInfo,int>,IEquipmentInfoRepository
    {
        private readonly DbContext dbContext;

        public EquipmentInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
