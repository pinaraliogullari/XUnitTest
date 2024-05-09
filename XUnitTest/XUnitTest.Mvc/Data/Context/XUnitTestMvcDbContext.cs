using Microsoft.EntityFrameworkCore;
using System.Reflection;
using XUnitTest.Mvc.Data.Entities;

namespace XUnitTest.Mvc.Data.Context
{
	public class XUnitTestMvcDbContext : DbContext
	{
		public XUnitTestMvcDbContext(DbContextOptions options) : base(options)
		{
		}

        public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}
