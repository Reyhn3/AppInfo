// This demonstrates how the AppInfo can be used to log information about the
// application at startup and also provide it when requested.


using AppInfo;

var appInfo = AppInfo.Create
	.DefaultBuilder()
	.Build()
	.WithDefaultOutput()
	.Write();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(appInfo);

var app = builder.Build();
app.UseHttpsRedirection();

// ! WARNING! This might expose sensitive information about the application.
// ! This code is only intended for demonstration purposes and should not be
// ! used in production.
app.MapGet("/", context =>
//TODO: #28: Consider adding a web renderer
	context.Response.WriteAsJsonAsync(
		context.RequestServices.GetRequiredService<IAppInfo>().Fragments));

app.Run();
