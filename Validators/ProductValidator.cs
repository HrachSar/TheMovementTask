using FluentValidation;
using MyProject.DTOs;


namespace MyProject.Validators
{
    public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(p => p.CategoryIds)
                .NotNull()
                .Must(list => list.Count == 2 || list.Count == 3)
                .WithMessage("Product must have exactly 2 or 3 categories.");
        }
    }

    public class ProductUpdateDTOValidator : AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(p => p.CategoryIds)
                .NotNull()
                .Must(list => list.Count == 2 || list.Count == 3)
                .WithMessage("Product must have exactly 2 or 3 categories.");
        }
    }

}