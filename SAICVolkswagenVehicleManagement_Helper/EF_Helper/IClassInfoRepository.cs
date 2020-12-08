using SAICVolkswagenVehicleManagement_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper.EF_Helper
{
    public interface IClassInfoRepository : IRepositoryBase1<ClassInfo>,IRepositoryBase2<ClassInfo,int>
    {
    }
}
