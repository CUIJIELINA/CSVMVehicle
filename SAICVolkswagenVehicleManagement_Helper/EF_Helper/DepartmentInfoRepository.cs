using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class DepartmentInfoRepository : RepositoryBase<DepartmentInfo,int>,IDepartmentInfoRepository
    {
        private readonly DbContext dbContext;

        public DepartmentInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
