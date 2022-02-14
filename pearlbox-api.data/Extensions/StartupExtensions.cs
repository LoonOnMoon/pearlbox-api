using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

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
			services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<PearlboxContext>();
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
					{
						options.SlidingExpiration = true;
						options.ExpireTimeSpan = new TimeSpan(0, 1, 0);
					});
		}
	}
}