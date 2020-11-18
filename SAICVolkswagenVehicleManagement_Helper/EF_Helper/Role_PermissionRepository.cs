using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class Role_PermissionRepository : RepositoryBase<Role_Permission,int>,IRole_PermissionRepository
    {
        private readonly DbContext dbContext;

        public Role_PermissionRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
