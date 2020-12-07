using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public interface IAbilityInfoRepository : IRepositoryBase1<AbilityInfo>,IRepositoryBase2<AbilityInfo,int>
    {
    }
}
