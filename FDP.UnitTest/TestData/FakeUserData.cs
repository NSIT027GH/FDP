using FDP.Infrastructure.Models;
using FDP.Shared;

namespace FDP.UnitTest;
public class FakeUserData
{
    public static List<User> GetUsersFakeData()
    {
        return [
                new User {
                    UserId = 1,
                    FirstName = "firstName1",
                    LastName = "lastName1",
                    Email = "person1@example.com",
                    Password = "person1@123",
                    PhoneNumber = 9876543211,
                    Role = 2,
                    Status = (int)TaskStatusEnum.UserStatus.Active

                },
                new User
                {
                    UserId = 2,
                    FirstName = "firstName2",
                    LastName = "lastName2",
                    Email = "person2@example.com",
                    Password = "person2@123",
                    PhoneNumber = 9876543212,
                    Role = 2,
                    Status = (int)TaskStatusEnum.UserStatus.Active
                },
        ];
    }
}