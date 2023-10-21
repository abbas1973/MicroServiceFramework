using Application.Validators;
using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class MediaFileCreateCommandValidator : AbstractValidator<MediaFileCreateCommand>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");
        private string _errorMaxLength = string.Format(Messages.ErrorMaxLength, "{PropertyName}", "{MaxLength}");
        private string _errorGreaterThan = string.Format(Messages.ErrorGreaterThan, "{PropertyName}", "{ComparisonValue}");

        public MediaFileCreateCommandValidator()
        {
            RuleFor(x => x.TitleFa).NotEmpty().WithMessage(_errorRequired)
                .MaximumLength(300).WithMessage(_errorMaxLength);

            RuleFor(x => x.TitleEn).NotEmpty().WithMessage(_errorRequired)
                .MaximumLength(300).WithMessage(_errorMaxLength);

            RuleFor(x => x.FileName).NotEmpty().WithMessage(_errorRequired)
                .MaximumLength(300).WithMessage(_errorMaxLength);

            RuleFor(x => x.Size).NotNull().WithMessage(_errorRequired)
                .GreaterThan(0).WithMessage(_errorGreaterThan);

            RuleFor(x => x.Format).NotNull().WithMessage(_errorRequired);

            RuleFor(x => x.Group).NotNull().WithMessage(_errorRequired);
        }
    }
}
