using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Model
{
    public class Base_DbContext : DbContext
    {
        public Base_DbContext(DbContextOptions<Base_DbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<CarBrandInfo> CarBrandInfo { get; set; }
        public DbSet<CarInfo> CarInfo { get; set; }
        public DbSet<DepartmentInfo> DepartmentInfo { get; set; }
        public DbSet<DriverInfo> DriverInfo { get; set; }
        public DbSet<EquipmentInfo> EquipmentInfo { get; set; }
        public DbSet<GPSPositioningInfo> GPSPositioningInfo { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<R_UserInfo> R_UserInfo { get; set; }
        public DbSet<Role_Permission> Role_Permission { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }
        public DbSet<TestSiteAndCar> TestSiteAndCar { get; set; }
        public DbSet<TestSiteInfo> TestSiteInfo { get; set; }
        public DbSet<User_Role> User_Role { get; set; }
        public DbSet<AbilityInfo> AbilityInfo { get; set; }
        public DbSet<ClassInfo> ClassInfo { get; set; }
        public DbSet<VehicleParameters> VehicleParameters { get; set; }
    }
}
