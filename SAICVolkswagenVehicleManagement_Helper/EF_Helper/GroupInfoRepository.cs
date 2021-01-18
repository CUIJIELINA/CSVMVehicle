using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public class GroupInfoRepository : RepositoryBase<GroupInfo,int>,IGroupInfoRepository
    {
        private readonly DbContext dbContext;

        public GroupInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
