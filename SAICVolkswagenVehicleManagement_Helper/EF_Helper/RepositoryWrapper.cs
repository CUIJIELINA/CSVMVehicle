using SAICVolkswagenVehicleManagement_Helper.EF_Helper;
using SAICVolkswagenVehicleManagement_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly Base_DbContext base_DbContext;

        public RepositoryWrapper(Base_DbContext base_DbContext)
        {
            this.base_DbContext = base_DbContext;
        }

        public IUser_RoleRepository user_RoleRepository => new User_RoleRepository(base_DbContext);

        public ICarBrandInfoRepository carBrandInfoRepository => new CarBrandInfoRepository(base_DbContext);

        public ICarInfoRepository carInfoRepository => new CarInfoRepository(base_DbContext);

        public IDepartmentInfoRepository departmentInfoRepository => new DepartmentInfoRepository(base_DbContext);

        public IDriverInfoRepository driverInfoRepository => new DriverInfoRepository(base_DbContext);

        public IEquipmentInfoRepository equipmentInfoRepository => new EquipmentInfoRepository(base_DbContext);

        public IPermissionRepository permissionRepository => new PermissionRepository(base_DbContext);

        public IR_UserInfoRepository r_UserInfoRepository => new R_UserInfoRepository(base_DbContext);

        public ITestSiteAndCarRepository testSiteAndCarRepository => new TestSiteAndCarRepository(base_DbContext);

        public ITestSiteInfoRepository testSiteInfoRepository => new TestSiteInfoRepository(base_DbContext);

        public IGPSPositioningInfoRepository gPSPositioningInfoRepository => new GPSPositioningInfoRepository(base_DbContext);

        public IRoleInfoRepository roleInfoRepository => new RoleInfoRepository(base_DbContext);

        public IRole_PermissionRepository role_PermissionRepository => new Role_PermissionRepository(base_DbContext);

        public IAbilityInfoRepository abilityInfoRepository => new AbilityInfoRepository(base_DbContext);

        public IClassInfoRepository classInfoRepository => new ClassInfoRepository(base_DbContext);

        public IVehicleParametersRepository vehicleParametersRepository => new VehicleParametersRepository(base_DbContext);
    }
}
