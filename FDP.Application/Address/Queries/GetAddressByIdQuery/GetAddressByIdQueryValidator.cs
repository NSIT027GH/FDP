using FluentValidation;

namespace FDP.Application.Address
{
    public class GetAddressByIdQueryValidator : AbstractValidator<GetAddressByIdQuery>
    {
        public GetAddressByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0).WithMessage("Invalid User Id");
        }
    }
}
