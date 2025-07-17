using FDP.Infrastructure.Models;
using System.Reflection.Metadata.Ecma335;

namespace FDP.UnitTest;

public class FakeCountryData
{
    public static List<Country> GetFakeCountryData()
    {
        return [
            new () { 
                CountryId = 1,
                Name = "India",
                Code = "IN",
                Phonecode = 91
            }
        ];
    }
}
