using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.Dtos;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(default(int))
            .WithMessage("El Id del producto es necesario.");

        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("El nombres de producto es necesario.");

        RuleFor(x => x.ProductType)
            .NotEmpty()
            .WithMessage("El tipo de producto es necesario.");

        RuleFor(x => x.NumTerminal)
            .NotEmpty()
            .WithMessage("El número de terminal es necesario.");

        RuleFor(x => x.SoldAt)
            .NotEmpty()
            .WithMessage("SoldAt es necesario."); ;
    }
}
