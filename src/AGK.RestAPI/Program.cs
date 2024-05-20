using AGK.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddInfrastructure(builder.Configuration)
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddControllers();

var app = builder.Build();

app.UseInfrastructure();

app
	.UseDefaultFiles()
	.UseStaticFiles()
	.UseHttpsRedirection()
	.UseExceptionHandler("/error");

app.UseRouting();
app.MapControllers();

if(app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.Run();
