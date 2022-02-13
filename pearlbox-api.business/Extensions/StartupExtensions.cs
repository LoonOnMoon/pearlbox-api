using Microsoft.Extensions.DependencyInjection;

namespace pearlbox_api.business.Extensions
{
    public class StartupExtensions
    {
		public static void ConfigureServices(IServiceCollection services, string EnvironmentName)
		{
			data.Extensions.StartupExtensions.ConfigureServices(services, EnvironmentName);

			// services.AddScoped<IQuestionService, QuestionService>();
			// services.AddScoped<IAnswerService, AnswerService>();
			// services.AddScoped<ITestService, TestService>();
			// services.AddScoped<ICategoryService, CategoryService>();
			// services.AddScoped<IEmailService, EmailService>();
			// services.AddScoped<IUserService, UserService>();
			// services.AddScoped<IInviteService, InviteService>();
			// services.AddScoped<IResultService, ResultService>();
			// // TODO: add all missing services

			// var mapperConfiguration = new MapperConfiguration(mc =>
			// {
			// 	mc.AddProfile(new MapperProfile());
			// });

			// IMapper mapper = mapperConfiguration.CreateMapper();
			// services.AddSingleton(mapper);

			//services.AddLocalization(options => options.ResourcesPath = "Infrastructure/Resources");

		}
	}
}