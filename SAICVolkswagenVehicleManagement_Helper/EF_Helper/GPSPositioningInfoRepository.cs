using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;
using Microsoft.EntityFrameworkCore;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class GPSPositioningInfoRepository : RepositoryBase<GPSPositioningInfo,int>,IGPSPositioningInfoRepository
    {
        private readonly DbContext dbContext;

        public GPSPositioningInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
