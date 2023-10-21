using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class DownloadByNameQueryValidator : AbstractValidator<DownloadByNameQuery>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public DownloadByNameQueryValidator()
        {
            RuleFor(x => x.FileName).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired);


            RuleFor(x => x.Group).NotNull().WithMessage(_errorRequired);

            RuleFor(x => x.IsPic).NotNull().WithMessage(_errorRequired);
        }
    }
}
