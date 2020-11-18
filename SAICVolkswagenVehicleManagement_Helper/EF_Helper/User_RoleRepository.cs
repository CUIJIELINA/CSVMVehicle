using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class User_RoleRepository : RepositoryBase<User_Role,int>,IUser_RoleRepository
    {
        private readonly DbContext dbContext;

        public User_RoleRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
