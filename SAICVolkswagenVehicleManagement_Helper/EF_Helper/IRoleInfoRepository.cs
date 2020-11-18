using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IRoleInfoRepository : IRepositoryBase1<RoleInfo>,IRepositoryBase2<RoleInfo,int>
    {
    }
}
