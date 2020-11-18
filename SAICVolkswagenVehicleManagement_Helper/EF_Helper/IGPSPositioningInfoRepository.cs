using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IGPSPositioningInfoRepository : IRepositoryBase1<GPSPositioningInfo>,IRepositoryBase2<GPSPositioningInfo,int>
    {
    }
}
