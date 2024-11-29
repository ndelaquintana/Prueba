using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

public class ClientAppService : DefaultAppService<Client,ClientDto>, IClientAppService
{
    public ClientAppService(
        ICrudRepository<Client> repository, 
        IValidator<ClientDto> dtoValidator,
        IMapper mapper):base(repository, dtoValidator, mapper)
    {
    }
    protected override void ValidateInsert(Client entity, ICrudRepository<Client> repository)
    {
        base.ValidateInsert(entity, repository);
        if (repository.GetAll(z=>z.DocType == entity.DocType && z.DocNum == z.DocNum).Any())
        {
            throw new Exception($"Ya existe {entity.DocType}{entity.DocNum}");
        }
    }
}