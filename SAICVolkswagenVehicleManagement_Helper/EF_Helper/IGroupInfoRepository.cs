using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public interface IGroupInfoRepository : IRepositoryBase1<GroupInfo>, IRepositoryBase2<GroupInfo, int>
    {
    }
}
