using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

internal class ProductAppService : IProductAppService
{
    private readonly ICrudRepository<Product> _repository;
    private readonly IValidator<ProductDto> _dtoValidator;
    private readonly IMapper _mapper;

    public ProductAppService(
        ICrudRepository<Product> repository, 
        IValidator<ProductDto> dtoValidator,
        IMapper mapper)
    {
        _repository = repository;
        _dtoValidator = dtoValidator;
        _mapper = mapper;
    }

    public IEnumerable<ProductDto> GetAll()
    {
        var entities = _repository.GetAll();
        return _mapper.Map<IEnumerable<ProductDto>>(entities);
    }

    public ProductDto GetById(params object[] keyValues)
    {
        var entity = _repository.GetById(keyValues);
        return _mapper.Map<ProductDto>(entity);
    }

    public void Create(ProductDto dto)
    {
        ValidateDto(dto);
        ValidateInsertDto(dto);
        var entity = _mapper.Map<Product>(dto);
        _repository.Create(entity);
    }

    public void Update(ProductDto dto)
    {
        ValidateDto(dto);
        ValidateUpdateDto(dto);
        var entity = _mapper.Map<Product>(dto);
        _repository.Update(entity);
    }

    public void Delete(ProductDto dto)
    {
        ValidateDeleteDto(dto);
        var entity = _mapper.Map<Product>(dto);
        _repository.Delete(entity);
    }

    private void ValidateDto(ProductDto dto)
    {
        var validationResult = _dtoValidator.Validate(dto);
        if (validationResult.Errors.Any())
        {
            string toShowErrors = string.Join("; ", validationResult.Errors.Select(s => s.ErrorMessage));
            throw new Exception($"El producto especificado no cumple los requisitos de validación. Errores: '{toShowErrors}'");
        }
    }

    private void ValidateInsertDto(ProductDto dto)
    {
        if (null != _repository.GetById(dto.Id))
        {
            throw new Exception($"Ya existe un producto con este Id");
        }
        if (_repository.GetAll(z=>z.ProductName == dto.ProductName).Any())
        {
            throw new Exception($"Ya existe un producto con nombre {dto.ProductName}");
        }
    }

    private void ValidateUpdateDto(ProductDto dto)
    {
        if (null == _repository.GetById(dto.Id))
        {
            throw new Exception("No existe ningún producto con este Id");
        }

        if (_repository.GetAll(
            z => z.ProductName == dto.ProductName && 
            z.Id != dto.Id
            ).Any())
        {
            throw new Exception($"Ya existe un producto con nombre {dto.ProductName}");
        }
    }

    private void ValidateDeleteDto(ProductDto dto)
    {
        if (null == _repository.GetById(dto.Id))
        {
            throw new Exception("No existe ningún producto con este Id");
        }
    }
}