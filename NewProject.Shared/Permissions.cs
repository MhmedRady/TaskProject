using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Shared;

public static class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permissions.{module}.View",
            $"Permissions.{module}.Create",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }
    public static class Departments
    {
        public const string View = "Permissions.Departments.View";
        public const string Create = "Permissions.Departments.Create";
        public const string Edit = "Permissions.Departments.Edit";
        public const string Delete = "Permissions.Departments.Delete";
    }

    public static class Vehicle
    {
        public const string View = "Permissions.Vehicle.View";
        public const string Create = "Permissions.Vehicle.Create";
        public const string Edit = "Permissions.Vehicle.Edit";
        public const string Delete = "Permissions.Vehicle.Delete";
    }

    public static class VehicleBrand
    {
        public const string View = "Permissions.VehicleBrand.View";
        public const string Create = "Permissions.VehicleBrand.Create";
        public const string Edit = "Permissions.VehicleBrand.Edit";
        public const string Delete = "Permissions.VehicleBrand.Delete";
    }

    public static class VehicleModel
    {
        public const string View = "Permissions.VehicleModel.View";
        public const string Create = "Permissions.VehicleModel.Create";
        public const string Edit = "Permissions.VehicleModel.Edit";
        public const string Delete = "Permissions.VehicleModel.Delete";
    }

    public static class VehicleType
    {
        public const string View = "Permissions.VehicleType.View";
        public const string Create = "Permissions.VehicleType.Create";
        public const string Edit = "Permissions.VehicleType.Edit";
        public const string Delete = "Permissions.VehicleType.Delete";
    }

    public static class Maintenance
    {
        public const string View = "Permissions.Maintenance.View";
        public const string Create = "Permissions.Maintenance.Create";
        public const string Edit = "Permissions.Maintenance.Edit";
        public const string Delete = "Permissions.Maintenance.Delete";
    }
    public static class MaintenanceRequest
    {
        public const string View = "Permissions.MaintenanceRequest.View";
        public const string Create = "Permissions.MaintenanceRequest.Create";
        public const string Edit = "Permissions.MaintenanceRequest.Edit";
        public const string Delete = "Permissions.MaintenanceRequest.Delete";
    }

    public static class Unit
    {
        public const string View = "Permissions.Unit.View";
        public const string Create = "Permissions.Unit.Create";
        public const string Edit = "Permissions.Unit.Edit";
        public const string Delete = "Permissions.Unit.Delete";
    }
    public static class SubUnit
    {
        public const string View = "Permissions.SubUnit.View";
        public const string Create = "Permissions.SubUnit.Create";
        public const string Edit = "Permissions.SubUnit.Edit";
        public const string Delete = "Permissions.SubUnit.Delete";
    }

    public static class Malfunction
    {
        public const string View = "Permissions.Malfunction.View";
        public const string Create = "Permissions.Malfunction.Create";
        public const string Edit = "Permissions.Malfunction.Edit";
        public const string Delete = "Permissions.Malfunction.Delete";
    }
    public static class CustodyTransferRecord
    {
        public const string View = "Permissions.CustodyTransferRecord.View";
        public const string Create = "Permissions.CustodyTransferRecord.Create";
        public const string Edit = "Permissions.CustodyTransferRecord.Edit";
        public const string Delete = "Permissions.CustodyTransferRecord.Delete";
    }
    public static class CustodyTransferRecordByUser
    {
        public const string View = "Permissions.CustodyTransferRecordByUser.View";
        public const string Create = "Permissions.CustodyTransferRecordByUser.Create";
        public const string Edit = "Permissions.CustodyTransferRecordByUser.Edit";
        public const string Delete = "Permissions.CustodyTransferRecordByUser.Delete";
    }



}
