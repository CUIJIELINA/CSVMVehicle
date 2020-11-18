using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class DriverInfoRepository : RepositoryBase<DriverInfo,int>,IDriverInfoRepository
    {
        private readonly DbContext dbContext;

        public DriverInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
