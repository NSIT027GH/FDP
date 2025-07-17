namespace FDP.Shared;

public class TaskStatusEnum
{
    public enum TaskResultStatus
    {
        Success = 1,
        Unsuccessful = 0
    }

    public enum PasswordResultStatus
    {
        TimeExpire = 2
    }

    public enum AddressStatus
    {
        DeleteAddress = 0,
        DefaultAddress = 1,
        NormalAddress = 2,
    }

    public enum UserStatus
    {
        Active = 1,
        Inactive = 0, 
        Admin = 2
    }
}