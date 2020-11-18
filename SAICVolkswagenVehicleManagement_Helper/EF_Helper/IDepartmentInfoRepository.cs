using SAICVolkswagenVehicleManagement_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IDepartmentInfoRepository : IRepositoryBase1<DepartmentInfo>,IRepositoryBase2<DepartmentInfo,int>
    {
    }
}
