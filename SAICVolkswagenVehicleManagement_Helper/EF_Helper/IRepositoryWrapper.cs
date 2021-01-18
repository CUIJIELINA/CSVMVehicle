using SAICVolkswagenVehicleManagement_Helper.EF_Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper
{
    public interface IRepositoryWrapper
    {
        IUser_RoleRepository user_RoleRepository { get; }
        ICarBrandInfoRepository carBrandInfoRepository { get; }
        ICarInfoRepository carInfoRepository { get; }
        IDepartmentInfoRepository departmentInfoRepository { get; }
        IDriverInfoRepository driverInfoRepository { get; }
        IEquipmentInfoRepository equipmentInfoRepository { get; }
        IPermissionRepository permissionRepository { get; }
        IR_UserInfoRepository r_UserInfoRepository { get; }
        ITestSiteAndCarRepository testSiteAndCarRepository { get; }
        ITestSiteInfoRepository testSiteInfoRepository { get; }
        IGPSPositioningInfoRepository gPSPositioningInfoRepository { get; }
        IRoleInfoRepository roleInfoRepository { get; }
        IRole_PermissionRepository role_PermissionRepository { get; }
        IAbilityInfoRepository abilityInfoRepository { get; }
        IClassInfoRepository classInfoRepository { get; }
        IVehicleParametersRepository vehicleParametersRepository { get; }
        IGroupInfoRepository groupInfoRepository { get; }
        IFaultRecordInfoRepository faultRecordInfoRepository { get; }

    }
}
