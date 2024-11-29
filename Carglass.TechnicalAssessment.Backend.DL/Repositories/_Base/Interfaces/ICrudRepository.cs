namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public interface ICrudRepository<TEntity>
{
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate);
    TEntity? GetById(params object[] keyValues);
    void Create(TEntity item);
    void Update(TEntity item);
    void Delete(TEntity item);
}