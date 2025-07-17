using FluentValidation;

namespace FDP.Application.Address
{
    public class GetAddressByUserIdQueryValidator : AbstractValidator<GetAddressByUserIdQuery>
    {
        public GetAddressByUserIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0).WithMessage("Invalid User Id");
        }
    }
}
