using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IRole_PermissionRepository : IRepositoryBase1<Role_Permission>,IRepositoryBase2<Role_Permission,int>
    {
    }
}
