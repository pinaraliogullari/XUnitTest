using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using XUnitTest.Mvc.Data.Context;

namespace XUnitTest.Mvc.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly XUnitTestMvcDbContext _context;
		public DbSet<TEntity> Table => _context.Set<TEntity>();
		public async Task CreateAsync(TEntity entity)
		{
			await Table.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public void DeleteAsync(TEntity entity)
		{
			  Table.Remove(entity);
			 _context.SaveChanges();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()=>await Table.ToListAsync();
		

		public async Task<TEntity> GetByIdAsync(Guid id)=> await Table.FindAsync(id);
		

		public void UpdateAsync(TEntity entity)
		{
		
		    EntityEntry entityEntry= Table.Update(entity);
			entityEntry.State = EntityState.Modified;
			_context.SaveChanges();
		}
	}
}
