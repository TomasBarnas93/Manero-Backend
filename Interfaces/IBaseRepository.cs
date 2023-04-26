namespace Manero_Backend.Interfaces
{
	public interface IBaseRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<TEntity> CreateAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TEntity entity);
		Task DeleteAsync(TEntity entity);
		Task<TEntity> GetByIdAsync(int id);
	}
}
