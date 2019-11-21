using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalAPI.Domain.Models;
using RentalAPI;

namespace RentalAPI.Infrastructure
{
    enum Prices { Premium = 150, Basic = 100 }

    public static class FakeDB
    {
        public static Dictionary<int, User> UsersTable()
        {
            if (_UsersTable == null)
            {
                InitUsersTableData();
            }
            return _UsersTable;
        }
        public static Dictionary<int, Vehicle> VehiclesTable()
        {
            if (_VehiclesTable == null)
            {
                InitVehiclesTableData();
            }
            return _VehiclesTable;
        }
        public static Dictionary<int, Rental> RentalsTable()
        {
            if (_RentalsTable == null)
            {
                InitRentalsTableData();
            }
            return _RentalsTable;
        }
        public static Dictionary<int, VehicleType> VehicleTypesTable()
        {
            if (_VehicleTypesTable == null)
            {
                InitVehicleTypesTableData();
            }
            return _VehicleTypesTable;
        }

        private static void InitUsersTableData()
        {
            _UsersTable = new Dictionary<int, User>();

            _UsersTable[1] = new User
            {
                Id = 1,
                Name = "Chillax",
                Password = Helpers.CreateMD5("1234")
            };

            _UsersTable[2] = new User
            {
                Id = 2,
                Name = "AnotherUser",
                Password = Helpers.CreateMD5("abcd")
            };
        }
        private static void InitVehiclesTableData()
        {
            _VehiclesTable = new Dictionary<int, Vehicle>();

            _VehiclesTable[1] = new Vehicle
            {
                Id = 1,
                Name = "Jaguar F - Type (1)",
                LicensePlate = "AT6331",
                CurrentlyRented = false,
                VehicleTypeId = 1
            };

            _VehiclesTable[2] = new Vehicle
            {
                Id = 2,
                Name = "Jaguar F - Type (2)",
                LicensePlate = "AB1234",
                CurrentlyRented = false,
                VehicleTypeId = 1
            };

            _VehiclesTable[3] = new Vehicle
            {
                Id = 3,
                Name = "BMW X7",
                LicensePlate = "XB1834",
                CurrentlyRented = false,
                VehicleTypeId = 2
            };

            _VehiclesTable[4] = new Vehicle
            {
                Id = 4,
                Name = "Audi Q7",
                LicensePlate = "UB8931",
                CurrentlyRented = false,
                VehicleTypeId = 2
            };

            _VehiclesTable[5] = new Vehicle
            {
                Id = 5,
                Name = "Lexus GX(1)",
                LicensePlate = "PB8874",
                CurrentlyRented = false,
                VehicleTypeId = 3
            };

            _VehiclesTable[6] = new Vehicle
            {
                Id = 6,
                Name = "Lexus GX(2)",
                LicensePlate = "PB8874",
                CurrentlyRented = false,
                VehicleTypeId = 3
            };


        }
        private static void InitRentalsTableData()
        {
            _RentalsTable = new Dictionary<int, Rental>();
        }
        private static void InitVehicleTypesTableData()
        {
            _VehicleTypesTable = new Dictionary<int, VehicleType>();

            VehicleType convertible = new VehicleType
            {
                Id = 1,
                Name = "Convertible",
                DailyCost = Price.Premium,
                DiscountAfterDays = int.MaxValue,
                DiscountedPrice = 1,
                PointsPerRental = 1.5f
            };
            _VehicleTypesTable[1] = convertible;

            VehicleType miniVan = new VehicleType
            {
                Id = 2,
                Name = "MiniVan",
                DailyCost = Price.Basic,
                DiscountAfterDays = 5,
                DiscountedPrice = 0.8f,
                PointsPerRental = 1.0f
            };
            _VehicleTypesTable[2] = miniVan;

            VehicleType suv = new VehicleType
            {
                Id = 3,
                Name = "SUV",
                DailyCost = Price.Basic,
                DiscountAfterDays = 3,
                DiscountedPrice = 0.7f,
                PointsPerRental = 1.0f
            };
            _VehicleTypesTable[3] = suv;
        }

        private static Dictionary<int, User> _UsersTable;
        private static Dictionary<int, Vehicle> _VehiclesTable;
        private static Dictionary<int, Rental> _RentalsTable;
        private static Dictionary<int, VehicleType> _VehicleTypesTable;

    }
}
