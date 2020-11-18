using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;
using Microsoft.EntityFrameworkCore;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class RoleInfoRepository : RepositoryBase<RoleInfo,int>,IRoleInfoRepository
    {
        private readonly DbContext dbContext;

        public RoleInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
