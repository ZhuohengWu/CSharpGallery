using FluentValidation;

namespace eCommerceClean.Application.Features.ProductDto.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
        }
    }
}
