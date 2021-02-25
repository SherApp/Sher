using FluentValidation;

namespace Sher.Application.Files.UploadFile
{
    public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.OriginalFileName).NotEmpty().WithMessage("OriginalFileName cannot be empty");
            RuleFor(x => x.FileStream).NotNull().WithMessage("FileStream cannot be null");
        }
    }
}