using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

public abstract class DefaultAppService<TEntity,TDto>
    (
        ICrudRepository<TEntity> repository,
        IValidator<TDto> dtoValidator,
        IMapper mapper
    )
    where TEntity : class, IEntity
    where TDto : class, IDto
{
    public IEnumerable<TDto> GetAll() =>
        mapper.Map<IEnumerable<TDto>>(repository.GetAll());
    
    public TDto GetById(params object[] keyValues) =>
        mapper.Map<TDto>(repository.GetById(keyValues));

    public void Create(TDto dto)
    {
        dtoValidator.ValidateAndShowIfAnyError(dto);
        if (mapper.Map<TEntity>(dto) is TEntity entity)
        {
            ValidateInsert(entity, repository);
            repository.Create(entity);
        }
    }

    public void Update(TDto dto)
    {
        dtoValidator.ValidateAndShowIfAnyError(dto);
        if (mapper.Map<TEntity>(dto) is TEntity entity)
        {
            ValidateUpdate(entity, repository);
            repository.Update(entity);
        }
    }

    public void Delete(TDto dto)
    {
        dtoValidator.Validate(dto);
        if (mapper.Map<TEntity>(dto) is TEntity entity)
        {
            ValidateDelete(entity, repository);
            repository.Delete(entity);
        }
    }

    protected virtual void ValidateInsert(TEntity entity, ICrudRepository<TEntity> repository)
    {
        if (repository.GetById(entity.Key) != null) 
            throw new Exception($"No se puede insertar ya existe");
    }

    protected virtual void ValidateUpdate(TEntity entity, ICrudRepository<TEntity> repository)
    {
        if (repository.GetById(entity.Key) == null)
            throw new Exception($"No se puede actualizar no existe");
    }

    protected virtual void ValidateDelete(TEntity entity, ICrudRepository<TEntity> repository)
    {
        if (repository.GetById(entity.Key) == null)
            throw new Exception($"No se puede eliminar no existe");
    }
}