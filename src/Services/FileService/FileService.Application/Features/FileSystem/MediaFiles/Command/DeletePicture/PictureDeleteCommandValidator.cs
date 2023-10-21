using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class PictureDeleteCommandValidator : AbstractValidator<PictureDeleteCommand>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public PictureDeleteCommandValidator()
        {
            RuleFor(x => x.FileName).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired);


            RuleFor(x => x.Group).NotNull().WithMessage(_errorRequired);
        }
    }
}
