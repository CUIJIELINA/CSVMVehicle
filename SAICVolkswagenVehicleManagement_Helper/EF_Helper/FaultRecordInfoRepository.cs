using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public class FaultRecordInfoRepository : RepositoryBase<FaultRecordInfo,int>,IFaultRecordInfoRepository
    {
        private readonly DbContext dbContext;

        public FaultRecordInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
