using FluentValidation;

namespace FDP.Application.Address
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotEqual(0).WithMessage("Invalid user id.");
            RuleFor(x => x.Location).NotEmpty().NotNull().WithMessage("Invalid Locaiton.");
            RuleFor(x => x.City).NotEmpty().NotNull().WithMessage("Invalid City.");
            RuleFor(x => x.Pincode).NotEmpty().NotNull().WithMessage("Invalid Pincode.");
            RuleFor(x => x.StateId).NotEmpty().NotNull().NotEqual(0).WithMessage("Invalid State.");
            RuleFor(x => x.CountryId).NotEmpty().NotNull().NotEqual(0).WithMessage("Invalid Country.");
        }
    }
}
