using SAICVolkswagenVehicleManagement_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public interface IVehicleParametersRepository : IRepositoryBase1<VehicleParameters>, IRepositoryBase2<VehicleParameters, int>
    {
    }
}
