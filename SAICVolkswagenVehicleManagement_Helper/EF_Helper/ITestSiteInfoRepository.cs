using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface ITestSiteInfoRepository : IRepositoryBase1<TestSiteInfo>,IRepositoryBase2<TestSiteInfo,int>
    {
    }
}
