using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using pearlbox_api.data.DatabaseObjects.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddSingleton(configuration);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidAudience = configuration["JWT:ValidAudience"],
						ValidIssuer = configuration["JWT:ValidIssuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
					};
					// options.Events = new JwtBearerEvents
					// {
					// 	OnAuthenticationFailed = context => {
					// 		Console.WriteLine(context.Exception);
					// 		return Task.CompletedTask;
					// 	}
					// };
				});

			services.AddDbContext<PearlboxContext>(options =>
				options.UseNpgsql(
					configuration.GetConnectionString("PostgresqlPearlbox")));

			services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddRoles<Role>()
				.AddUserManager<UserManager<User>>()
				.AddRoleManager<RoleManager<Role>>()
				.AddEntityFrameworkStores<PearlboxContext>()
				.AddDefaultTokenProviders();
			
		}
	}
}