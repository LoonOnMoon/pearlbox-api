using Microsoft.Extensions.DependencyInjection;

using AutoMapper;

using pearlbox_api.business.Services;
using pearlbox_api.business.Infrastructure.Mapper;

namespace pearlbox_api.business.Infrastructure.Extensions
{
    public class StartupExtensions
    {
		public static void ConfigureServices(IServiceCollection services, string EnvironmentName, Profile webMapperProfile)
		{
			data.Extensions.StartupExtensions.ConfigureServices(services, EnvironmentName);

			services.AddScoped<IAuthenticationService, AuthenticationService>();

			var mapperConfiguration = new MapperConfiguration(mc =>
			{
				mc.AddProfile(webMapperProfile);
				mc.AddProfile(new BusinessMapperProfile());
			});

			IMapper mapper = mapperConfiguration.CreateMapper();
			services.AddSingleton(mapper);

			//services.AddLocalization(options => options.ResourcesPath = "Infrastructure/Resources");

		}
	}
}