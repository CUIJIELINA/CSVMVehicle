using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;
using Microsoft.EntityFrameworkCore;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class PermissionRepository : RepositoryBase<Permission,int>,IPermissionRepository
    {
        private readonly DbContext dbContext;

        public PermissionRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
