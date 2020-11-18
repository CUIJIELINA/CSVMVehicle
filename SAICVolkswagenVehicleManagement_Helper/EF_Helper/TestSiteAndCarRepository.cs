using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class TestSiteAndCarRepository : RepositoryBase<TestSiteAndCar,int>,ITestSiteAndCarRepository
    {
        private readonly DbContext dbContext;

        public TestSiteAndCarRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
