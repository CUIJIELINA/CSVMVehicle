using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class CarBrandInfoRepository : RepositoryBase<CarBrandInfo, int>, ICarBrandInfoRepository
    {
        private readonly DbContext dbContext;

        public CarBrandInfoRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
