using System.Net;
using Newtonsoft.Json;
using Serilog;

namespace pearlbox_api.web.Infrastructure.Middleware
{
	public class ExceptionHandlingMiddleware
	{
		private RequestDelegate nextRequestDelegate;
		private IWebHostEnvironment environment;

		public ExceptionHandlingMiddleware(
			RequestDelegate nextRequestDelegate,
			IWebHostEnvironment environment
		)
		{
			this.nextRequestDelegate = nextRequestDelegate ?? throw new ArgumentNullException(nameof(nextRequestDelegate));
			this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				context.TraceIdentifier = Guid.NewGuid().ToString();
				await this.nextRequestDelegate(context);
			}
			catch (Exception ex)
			{
				await this.HandleGlobalExceptionAsync(context, ex);
			}
		}

		private async Task HandleGlobalExceptionAsync(HttpContext context, Exception ex)
		{
			try
			{
				await this.WriteResponseAsync(context, HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(ex.Message));
			}
			catch (Exception e)
			{
				await this.WriteResponseAsync(context, HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(e.Message));
			}

			var logger = new LoggerConfiguration()
								  .MinimumLevel.Debug()
								  .WriteTo.File(@"./Logs/error-.log", rollingInterval: RollingInterval.Day)
								  .CreateLogger();
			logger.Error(ex, "Unhandled Exception"/* - Detected UserId: {UserId}" /*, context.User.GetUserId()*/);
		}

		private async Task WriteResponseAsync(HttpContext context, HttpStatusCode code, string jsonResponse)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;
			await context.Response.WriteAsync(jsonResponse);
		}
	}
}