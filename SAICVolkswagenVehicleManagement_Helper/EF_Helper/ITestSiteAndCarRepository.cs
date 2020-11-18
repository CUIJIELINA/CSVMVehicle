using System;
using System.Collections.Generic;
using System.Text;
using SAICVolkswagenVehicleManagement_Model;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface ITestSiteAndCarRepository : IRepositoryBase1<TestSiteAndCar>,IRepositoryBase2<TestSiteAndCar,int>
    {
    }
}
