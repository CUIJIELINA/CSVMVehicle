using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IEquipmentInfoRepository : IRepositoryBase1<EquipmentInfo>,IRepositoryBase2<EquipmentInfo,int>
    {
    }
}
