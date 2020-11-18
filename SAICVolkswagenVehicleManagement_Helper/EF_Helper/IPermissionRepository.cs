using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IPermissionRepository : IRepositoryBase1<Permission>,IRepositoryBase2<Permission,int>
    {
    }
}
