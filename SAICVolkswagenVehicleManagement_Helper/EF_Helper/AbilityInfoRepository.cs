using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public class AbilityInfoRepository : RepositoryBase<AbilityInfo,int>,IAbilityInfoRepository
    {
        private readonly DbContext dbContext;

        public AbilityInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
