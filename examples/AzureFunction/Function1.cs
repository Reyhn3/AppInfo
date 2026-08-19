using AppInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;


namespace Functions;


public class DemoFunction(ILogger<DemoFunction> logger, IAppInfo appInfo)
{
	[Function("DemoFunction")]
	public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
	{
		logger.LogInformation("C# HTTP trigger function processed a request.");

		// ! WARNING! This might expose sensitive information about the application.
		// ! This code is only intended for demonstration purposes and should not be
		// ! used in production.
		return new OkObjectResult(appInfo.Fragments);
	}
}
