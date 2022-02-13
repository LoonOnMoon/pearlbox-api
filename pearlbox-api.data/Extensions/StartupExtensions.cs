using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace pearlbox_api.data.Extensions
{
	public class StartupExtensions
	{
		public static void ConfigureServices(IServiceCollection services, string EnvironmentName)
		{
			
			var configuration = new ConfigurationBuilder()
				.AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
        		.AddJsonFile(path: $"appsettings.{EnvironmentName}.json", optional: true)
				.Build();
			services.AddDbContext<PearlboxContext>(options =>
				options.UseNpgsql(
					configuration.GetConnectionString("PostgresqlPearlbox")));
		}
	}
}