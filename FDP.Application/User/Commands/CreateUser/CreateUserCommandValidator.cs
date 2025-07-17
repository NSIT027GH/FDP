using FDP.Lib;
using FluentValidation;

namespace FDP.Application.User.Command.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserService _userService;
        public CreateUserCommandValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.FirstName).NotEmpty().NotNull().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().NotNull().MaximumLength(75);
            RuleFor(x => x.Email).NotEmpty().NotNull().MustAsync(EmailExist).WithMessage("Email already exists.");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().MustAsync(PhoneNumberExist).WithMessage("PhoneNumber already exists.");
            RuleFor(x => x.Password)
           .NotEmpty()
           .MinimumLength(8)
           .MaximumLength(16)
           .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")
           .WithMessage("Password must be 8–16 characters long, with at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        }

        private async Task<bool> EmailExist(string email, CancellationToken cancellationToken)
        {
            return !await _userService.CheckMailExist(email);
        }
        private async Task<bool> PhoneNumberExist(long number, CancellationToken cancellationToken)
        {
            return !await _userService.CheckPhoneNumberExist(number);
        }
    }
}