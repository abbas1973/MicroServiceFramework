using Application.Validators;
using FluentValidation;
using Resources;

namespace AuthServer.Application.Features.Users
{
    public class UserGetListQueryValidator : AbstractValidator<UserGetListQuery>
    {
        private string _errorGreaterThanOrEqualTo = string.Format(Messages.ErrorGreaterThanOrEqual, "{PropertyName}", "{ComparisonProperty}");

        public UserGetListQueryValidator()
        {
            Include(new IPaginationDTOValidator());

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage(_errorGreaterThanOrEqualTo);
        }


    }
}
