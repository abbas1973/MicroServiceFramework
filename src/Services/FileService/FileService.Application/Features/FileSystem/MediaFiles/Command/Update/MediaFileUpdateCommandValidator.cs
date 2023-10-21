using Application.Validators;
using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class MediaFileUpdateCommandValidator : AbstractValidator<MediaFileUpdateCommand>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");
        private string _errorMaxLength = string.Format(Messages.ErrorMaxLength, "{PropertyName}", "{MaxLength}");

        public MediaFileUpdateCommandValidator()
        {
            Include(new IBaseEntityDTOValidator());

            RuleFor(x => x.TitleFa).NotEmpty().WithMessage(_errorRequired)
                .MaximumLength(300).WithMessage(_errorMaxLength);

            RuleFor(x => x.TitleEn).NotEmpty().WithMessage(_errorRequired)
                .MaximumLength(300).WithMessage(_errorMaxLength);

        }
    }
}
