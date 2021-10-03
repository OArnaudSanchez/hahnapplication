using FluentValidation;
using Hahn.ApplicatonProcess.July2021.Domain.DTOs;

namespace Hahn.ApplicatonProcess.July2021.Domain.Validators
{
    public class AssetValidator : AbstractValidator<AssetDTO>
    {
        public AssetValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Symbol).NotNull().NotEmpty();            
        }
    }
}
