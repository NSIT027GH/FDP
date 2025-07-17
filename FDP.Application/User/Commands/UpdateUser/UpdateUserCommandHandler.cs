using FDP.Lib;
using FDP.Shared;
using MediatR;

namespace FDP.Application.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserService _iUserService; 

        public UpdateUserCommandHandler(IUserService userService)
        {
            _iUserService = userService;
        }

        public async Task<int> Handle(UpdateUserCommand command, CancellationToken ct)
        {
            var userUpdateRequest = new UserUpdateRequestModel
            {
                UserId = command.UserId,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Password = command.Password,
                //Status = (int)TaskStatusEnum.UserStatus.Active
            };

            var result = await _iUserService.UpdateUser(userUpdateRequest);
            return result;
        }
    }
}
