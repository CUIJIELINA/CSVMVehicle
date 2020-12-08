using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public class ClassInfoRepository : RepositoryBase<ClassInfo,int>,IClassInfoRepository
    {
        private readonly DbContext dbContext;

        public ClassInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
