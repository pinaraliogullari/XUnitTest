﻿namespace XUnitTest.Mvc.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<TEntity> GetByIdAsync(Guid id);
		Task CreateAsync(TEntity entity);
		void UpdateAsync(TEntity entity);
		void DeleteAsync(TEntity entity);

	}
}
