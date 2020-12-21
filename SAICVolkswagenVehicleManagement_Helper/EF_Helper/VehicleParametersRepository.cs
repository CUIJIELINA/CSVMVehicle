using Microsoft.EntityFrameworkCore;
using SAICVolkswagenVehicleManagement_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public class VehicleParametersRepository : RepositoryBase<VehicleParameters, int>, IVehicleParametersRepository
    {
        private readonly DbContext dbContext;

        public VehicleParametersRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
