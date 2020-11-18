using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IUser_RoleRepository : IRepositoryBase1<User_Role>,IRepositoryBase2<User_Role,int>
    {
    }
}
