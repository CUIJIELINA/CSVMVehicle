using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IDriverInfoRepository : IRepositoryBase1<DriverInfo>,IRepositoryBase2<DriverInfo,int>
    {
    }
}
