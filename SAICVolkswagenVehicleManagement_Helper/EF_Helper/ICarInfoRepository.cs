using SAICVolkswagenVehicleManagement_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface ICarInfoRepository : IRepositoryBase1<CarInfo>,IRepositoryBase2<CarInfo,int>
    {
    }
}
