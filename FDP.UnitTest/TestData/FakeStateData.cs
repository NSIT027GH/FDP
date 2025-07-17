using FDP.Infrastructure.Models;
namespace FDP.UnitTest;

public class FakeStateData
{
    public static List<State> GetStateFakeData()
    {
        return
        [
            new() { StateId = 1,  CountryId = 1, Name = "Andhra Pradesh" },
            new() { StateId = 2,  CountryId = 1, Name = "Arunachal Pradesh" },
            new() { StateId = 3,  CountryId = 1, Name = "Assam" },
            new() { StateId = 4,  CountryId = 1, Name = "Bihar" },
            new() { StateId = 5,  CountryId = 1, Name = "Chhattisgarh" },
            new() { StateId = 6,  CountryId = 1, Name = "Goa" },
            new() { StateId = 7,  CountryId = 1, Name = "Gujarat" },
            new() { StateId = 8,  CountryId = 1, Name = "Haryana" },
            new() { StateId = 9,  CountryId = 1, Name = "Himachal Pradesh" },
            new() { StateId = 10, CountryId = 1, Name = "Jharkhand" },
            new() { StateId = 13, CountryId = 1, Name = "Karnataka" },
            new() { StateId = 14, CountryId = 1, Name = "Kerala" },
            new() { StateId = 15, CountryId = 1, Name = "Madhya Pradesh" },
            new() { StateId = 16, CountryId = 1, Name = "Maharashtra" },
            new() { StateId = 17, CountryId = 1, Name = "Manipur" },
            new() { StateId = 18, CountryId = 1, Name = "Meghalaya" },
            new() { StateId = 19, CountryId = 1, Name = "Mizoram" },
            new() { StateId = 20, CountryId = 1, Name = "Nagaland" },
            new() { StateId = 21, CountryId = 1, Name = "Odisha" },
            new() { StateId = 22, CountryId = 1, Name = "Punjab" },
            new() { StateId = 23, CountryId = 1, Name = "Rajasthan" },
            new() { StateId = 24, CountryId = 1, Name = "Sikkim" },
            new() { StateId = 25, CountryId = 1, Name = "Tamil Nadu" },
            new() { StateId = 27, CountryId = 1, Name = "Telangana" },
            new() { StateId = 28, CountryId = 1, Name = "Tripura" },
            new() { StateId = 29, CountryId = 1, Name = "Uttar Pradesh" },
            new() { StateId = 30, CountryId = 1, Name = "Uttarakhand" },
            new() { StateId = 31, CountryId = 1, Name = "West Bengal" },
        ];
    }
}
