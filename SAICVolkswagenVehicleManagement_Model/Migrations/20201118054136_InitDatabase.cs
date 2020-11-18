using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAICVolkswagenVehicleManagement_Model.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBrandInfo",
                columns: table => new
                {
                    CarBrandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarBrandCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarBrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarBrandDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrandInfo", x => x.CarBrandID);
                });

            migrationBuilder.CreateTable(
                name: "CarInfo",
                columns: table => new
                {
                    CarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarType = table.Column<int>(type: "int", nullable: false),
                    CarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarBrandID = table.Column<int>(type: "int", nullable: false),
                    CarPicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonLiable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUse = table.Column<int>(type: "int", nullable: false),
                    CarDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EquipmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInfo", x => x.CarID);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentInfo",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpID = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentInfo", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "DriverInfo",
                columns: table => new
                {
                    DriverID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverSex = table.Column<int>(type: "int", nullable: false),
                    DriverNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverType = table.Column<int>(type: "int", nullable: false),
                    DriverState = table.Column<int>(type: "int", nullable: false),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    IsState = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuasiDriving = table.Column<int>(type: "int", nullable: false),
                    DriverRemark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverInfo", x => x.DriverID);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentInfo",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentState = table.Column<int>(type: "int", nullable: false),
                    EquipmentRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentInfo", x => x.EquipmentID);
                });

            migrationBuilder.CreateTable(
                name: "GPSPositioningInfo",
                columns: table => new
                {
                    GPSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    GPSCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    GPSState = table.Column<int>(type: "int", nullable: false),
                    LocationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Angle = table.Column<double>(type: "float", nullable: false),
                    CurrentMileage = table.Column<double>(type: "float", nullable: false),
                    TotalMileage = table.Column<double>(type: "float", nullable: false),
                    GPSDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPSSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPSPositioningInfo", x => x.GPSID);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    PermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpID = table.Column<int>(type: "int", nullable: false),
                    PermissionUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                });

            migrationBuilder.CreateTable(
                name: "R_UserInfo",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPassWord = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Department_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_R_UserInfo", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Role_Permission",
                columns: table => new
                {
                    ConnectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    PermissionID = table.Column<int>(type: "int", nullable: false),
                    Permission = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Permission", x => x.ConnectionID);
                });

            migrationBuilder.CreateTable(
                name: "RoleInfo",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInfo", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "TestSiteAndCar",
                columns: table => new
                {
                    TestSiteAndCarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    TestSiteID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSiteAndCar", x => x.TestSiteAndCarID);
                });

            migrationBuilder.CreateTable(
                name: "TestSiteInfo",
                columns: table => new
                {
                    TestSiteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Contacts = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactsPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SpotSpeed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    AddressState = table.Column<int>(type: "int", nullable: false),
                    StartintPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminationPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    AddressIdentificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSiteInfo", x => x.TestSiteID);
                });

            migrationBuilder.CreateTable(
                name: "User_Role",
                columns: table => new
                {
                    ConnectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Role", x => x.ConnectionID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarBrandInfo");

            migrationBuilder.DropTable(
                name: "CarInfo");

            migrationBuilder.DropTable(
                name: "DepartmentInfo");

            migrationBuilder.DropTable(
                name: "DriverInfo");

            migrationBuilder.DropTable(
                name: "EquipmentInfo");

            migrationBuilder.DropTable(
                name: "GPSPositioningInfo");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "R_UserInfo");

            migrationBuilder.DropTable(
                name: "Role_Permission");

            migrationBuilder.DropTable(
                name: "RoleInfo");

            migrationBuilder.DropTable(
                name: "TestSiteAndCar");

            migrationBuilder.DropTable(
                name: "TestSiteInfo");

            migrationBuilder.DropTable(
                name: "User_Role");
        }
    }
}
