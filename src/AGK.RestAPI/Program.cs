using AGK.DataAccess.Migrations;
using AGK.Infrastructure;
using AGK.Infrastructure.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddInfrastructure(builder.Configuration)
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddControllers(options => {
		options.ModelBinderProviders.Insert(0, new PageNumberModelBinderProvider());
	});

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

app.SeedData();

app.Run();
