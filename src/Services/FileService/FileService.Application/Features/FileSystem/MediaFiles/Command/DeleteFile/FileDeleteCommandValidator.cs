using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class FileDeleteCommandValidator : AbstractValidator<FileDeleteCommand>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public FileDeleteCommandValidator()
        {
            RuleFor(x => x.FileName).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired);


            RuleFor(x => x.Group).NotNull().WithMessage(_errorRequired);
        }
    }
}
