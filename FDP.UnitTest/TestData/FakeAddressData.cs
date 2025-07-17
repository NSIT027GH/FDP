using FDP.Infrastructure.Models;
using FDP.Shared;

namespace FDP.UnitTest;
public class FakeAddressData
{
    public static List<Address> GetAddressFakeData()
    {
        return [
                new Address
                {
                    AddressId = 1,
                    UserId = 1,
                    Location = "location1",
                    Area = "area1",
                    City = "city1",
                    StateId = 16,
                    CountryId = 1,
                    Pincode = 100001,
                    Status = (int)TaskStatusEnum.AddressStatus.DefaultAddress
                },
                new Address
                {
                    AddressId = 2,
                    UserId = 1,
                    Location = "location2",
                    Area = "area2",
                    City = "city2",
                    StateId = 16,
                    CountryId = 1,
                    Pincode = 100002,
                    Status = (int)TaskStatusEnum.AddressStatus.NormalAddress
                },
                new Address
                {
                    AddressId = 3,
                    UserId = 1,
                    Location = "location3",
                    Area = "area3",
                    City = "city3",
                    StateId = 16,
                    CountryId = 1,
                    Pincode = 100003,
                    Status = (int)TaskStatusEnum.AddressStatus.NormalAddress
                },
                new Address
                {
                    AddressId = 4,
                    UserId = 2,
                    Location = "location4",
                    Area = "area4",
                    City = "city4",
                    StateId = 16,
                    CountryId = 1,
                    Pincode = 100004,
                    Status = (int)TaskStatusEnum.AddressStatus.NormalAddress
                }
        ];
    }
}