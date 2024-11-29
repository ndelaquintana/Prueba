using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

public class ProductAppService : DefaultAppService<Product, ProductDto>, IProductAppService
{
    public ProductAppService(
        ICrudRepository<Product> repository,
        IValidator<ProductDto> dtoValidator,
        IMapper mapper) : base(repository, dtoValidator, mapper)
    {
    }
    protected override void ValidateInsert(Product entity, ICrudRepository<Product> repository)
    {
        base.ValidateInsert(entity, repository);
        if (repository.GetAll(z => z.ProductName == entity.ProductName).Any())
        {
            throw new Exception($"Ya existe {entity.ProductName}");
        }
    }
}