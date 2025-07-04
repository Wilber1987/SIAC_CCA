using System.Text.Json.Serialization;
using BackgroundJob.Cron.Jobs;
using Microsoft.AspNetCore.ResponseCompression;
using APPCORE.Cron.Jobs;
using BusinessLogic.Connection;
using CAPA_NEGOCIO.Oparations;

//SqlADOConexion.IniciarConexion("sa", "**$NIcca24@$PX", "BDSRV\\SQLCCA", "SIAC_CCA_BEFORE_DEMO");

//AppGeneratorProgram.Main(); //generador de codigo

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddRazorPages();



builder.Services.AddControllers().AddJsonOptions(JsonOptions =>
		JsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null);

#region CONFIGURACIONES PARA API
builder.Services.AddControllers()
	.AddJsonOptions(JsonOptions => JsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null)// retorna los nombres reales de las propiedades
	.AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = false)// Desactiva la indentación
	.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddResponseCompression(options =>
{
	options.EnableForHttps = true; // Activa la compresión también para HTTPS
	options.Providers.Add<GzipCompressionProvider>(); // Usar Gzip
	options.Providers.Add<BrotliCompressionProvider>(); // Usar Brotli (más eficiente)
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
	options.Level = System.IO.Compression.CompressionLevel.Fastest; // Puedes ajustar la compresión
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
	options.Level = System.IO.Compression.CompressionLevel.Fastest; // Nivel de compresión para Brotli
});

#endregion


builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(40);
});


#region CRONJOB

/*builder.Services.AddCronJob<SendInvitationToUpdateCronJob>(options =>
{	
	options.CronExpression = "/4 8-20 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});*/

/*builder.Services.AddCronJob<UpdateDataBellacomCronJob>(options =>
{
	options.CronExpression = "0 4 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});*/

builder.Services.AddCronJob<SendMailNotificationsSchedulerJob>(options =>
{
	options.CronExpression = "*/4 * * * *";
	//options.CronExpression = "* * * * *";
	options.TimeZone = TimeZoneInfo.Local;
});

/***sincronizacion de siac y bellacom a sistema*/
/*builder.Services.AddCronJob<MigrateDocentesCronJob>(options =>
{
	options.CronExpression = "0 15 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});

builder.Services.AddCronJob<MigrateEstudiantesCronJob>(options =>
{
	options.CronExpression = "10 16 * * *";	
	options.TimeZone = TimeZoneInfo.Local;
});

builder.Services.AddCronJob<MigrateGestionCursosCronJob>(options =>
{
	options.CronExpression = "0 20 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});

builder.Services.AddCronJob<MigrateNotasCronJob>(options =>
{
	options.CronExpression = "0 1 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});
*/
#endregion

var app = builder.Build();
new BDConnection().IniciarMainConecction(app.Environment.IsDevelopment());
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}


/*await new MigrateDocentes().Migrate();
await new MigrateGestionCursos().Migrate();
await new MigrateEstudiantes().Migrate();
await new MigrateNotas().Migrate(null);*/


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseResponseCompression(); // Usa la compresión en la aplicación

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();
app.UseEndpoints(endpoints =>
	{
		endpoints.MapControllers();
		endpoints.MapRazorPages();
		endpoints.MapControllerRoute(
		   name: "default",
		   pattern: "{controller=Home}/{action=Login}/{id?}");
	});

app.Run();
