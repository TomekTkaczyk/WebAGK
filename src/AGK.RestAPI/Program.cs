using AGK.DataAccess.Migrations;
using AGK.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddInfrastructure(builder.Configuration)
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddControllers();

var app = builder.Build();

app.UseInfrastructure()
	.UseDefaultFiles()
	.UseStaticFiles()
	.UseHttpsRedirection()
	.UseRouting();

app.MapControllers();

if(app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
};

app.PerformMigrations();

app.Run();
