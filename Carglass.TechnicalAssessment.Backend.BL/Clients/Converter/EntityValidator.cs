using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.Entities;

public class EntityValidator<TEntity> : AbstractValidator<TEntity>
    where TEntity : class, IEntity
{
    public EntityValidator(ICrudRepository<TEntity> repository)
    {
        RuleFor(x => x.Key)
            .NotEmpty()
            .WithMessage("El tipo de documento es necesario.");
    }
}
