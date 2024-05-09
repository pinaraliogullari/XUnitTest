using Microsoft.EntityFrameworkCore;
using XUnitTest.Mvc.Data.Context;
using XUnitTest.Mvc.Repositories;

namespace XUnitTest.Mvc
{
	public static class ServiceRegistiration
	{
		public static void LoadMyServices(this IServiceCollection services)
		{
			services.AddDbContext<XUnitTestMvcDbContext>(options => options.UseSqlite(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("SqliteConnection")));
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		}
	}
}
