using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

public class ClientAppService : IClientAppService
{
    private readonly ICrudRepository<Client> _repository;
    private readonly IValidator<ClientDto> _dtoValidator;
    private readonly IMapper _mapper;
    public ClientAppService(
        ICrudRepository<Client> repository, 
        IValidator<ClientDto> dtoValidator,
        IMapper mapper)
    {
        _repository = repository;
        _dtoValidator = dtoValidator;
        _mapper = mapper;
    }

    public IEnumerable<ClientDto> GetAll()
    {
        var entities = _repository.GetAll();
        return _mapper.Map<IEnumerable<ClientDto>>(entities);
    }

    public ClientDto GetById(params object[] keyValues)
    {
        var entity = _repository.GetById(keyValues);
        return _mapper.Map<ClientDto>(entity);
    }

    public void Create(ClientDto dto)
    {
        ValidateDto(dto);
        ValidateInsertDto(dto);
        var entity = _mapper.Map<Client>(dto);
        _repository.Create(entity);
    }

    public void Update(ClientDto dto)
    {
        ValidateDto(dto);
        ValidateUpdateDto(dto);
        var entity = _mapper.Map<Client>(dto);
        _repository.Update(entity);
    }

    public void Delete(ClientDto dto)
    {
        ValidateDeleteDto(dto);
        var entity = _mapper.Map<Client>(dto);
        _repository.Delete(entity);
    }

    private void ValidateDto(ClientDto dto)
    {
        var validationResult = _dtoValidator.Validate(dto);
        if (validationResult.Errors.Any())
        {
            string toShowErrors = string.Join("; ", validationResult.Errors.Select(s => s.ErrorMessage));
            throw new Exception($"El cliente especificado no cumple los requisitos de validación. Errores: '{toShowErrors}'");
        }
    }

    private void ValidateInsertDto(ClientDto dto)
    {
        if (null != _repository.GetById(dto.Id))
        {
            throw new Exception("Ya existe un cliente con este Id");
        }
        if (_repository.GetAll(z=>z.DocNum == dto.DocNum).Any())
        {
            throw new Exception($"Ya existe un cliente con docNum {dto.DocNum}");
        }
    }

    private void ValidateUpdateDto(ClientDto dto)
    {
        if (null == _repository.GetById(dto.Id))
        {
            throw new Exception("No existe ningún cliente con este Id");
        }

        if (_repository.GetAll(
            z => z.DocNum == dto.DocNum && 
            z.Id != dto.Id
            ).Any())
        {
            throw new Exception($"Ya existe un cliente con docNum {dto.DocNum}");
        }
    }

    private void ValidateDeleteDto(ClientDto dto)
    {
        if (null == _repository.GetById(dto.Id))
        {
            throw new Exception("No existe ningún cliente con este Id");
        }
    }
}