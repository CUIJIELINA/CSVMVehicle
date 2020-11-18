using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class R_UserInfoRepository : RepositoryBase<R_UserInfo,int>,IR_UserInfoRepository
    {
        private readonly DbContext dbContext;

        public R_UserInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
