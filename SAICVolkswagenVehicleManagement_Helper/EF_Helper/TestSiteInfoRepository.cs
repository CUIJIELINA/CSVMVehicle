using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class TestSiteInfoRepository : RepositoryBase<TestSiteInfo,int>,ITestSiteInfoRepository
    {
        private readonly DbContext dbContext;

        public TestSiteInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
