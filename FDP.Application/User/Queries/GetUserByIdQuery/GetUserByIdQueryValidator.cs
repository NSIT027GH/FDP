using FluentValidation;

namespace FDP.Application.User;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0).WithMessage("Invalid User Id");
    }
}
