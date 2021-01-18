using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public interface IFaultRecordInfoRepository : IRepositoryBase1<FaultRecordInfo>, IRepositoryBase2<FaultRecordInfo, int>
    {
    }
}
