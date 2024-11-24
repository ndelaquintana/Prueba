using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public interface ICrudRepository<TEntity>
{
    IEnumerable<TEntity> GetAll(Func<TEntity, bool>? predicate = null);
    TEntity? GetById(params object[] keyValues);
    void Create(TEntity item);
    void Update(TEntity item);
    void Delete(TEntity item);
}
