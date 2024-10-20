using System.Text.Json.Serialization;
using CAPA_DATOS;
using CAPA_NEGOCIO.Gestion_Pagos.Model;
using CAPA_NEGOCIO.Gestion_Pagos.Operations;
using CAPA_NEGOCIO.Oparations;
using Microsoft.AspNetCore.ResponseCompression;

//coneccion wilber
//SqlADOConexion.IniciarConexion("sa", "**$NIcca24@$PX", "BDSRV\\SQLCCA", "OLIMPO");

//coneccion wilber

//SqlADOConexion.IniciarConexion("sa", "zaxscd", "localhost", "OLIMPO");
//MySQLConnection.IniciarConexion("root", "", "localhost", "siac_cca_production", 3306);

//coneccion alder

SqlADOConexion.IniciarConexion("sa", "123", "localhost\\SQLEXPRESS", "SIAC_CCA_BEFORE_DEMO");
//SqlADOConexion.IniciarConexion("sa", "**$NIcca24@$PX", "BDSRV\\SQLCCA", "SIAC_CCA_BEFORE_DEMO");
//AppGeneratorProgram.Main(); //generador de codigo


// Migraciones
//TODO crear conexion aparte para cronjob
/*await new MigrateDocentes().Migrate();
await new MigrateEstudiantes().Migrate();
await new MigrateGestionCursos().Migrate();
await new MigrateNotas().Migrate();*/



var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddRazorPages();

builder.Services.AddControllers().AddJsonOptions(JsonOptions =>
		JsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null);

#region CONFIGURACIONES PARA API
builder.Services.AddControllers()
	.AddJsonOptions(JsonOptions => JsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null)// retorna los nombres reales de las propiedades
	.AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = false)// Desactiva la indentación
	.AddJsonOptions(options =>  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
	
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
/*builder.Services.AddCronJob<DailyCronJob>(options =>
{	
	options.CronExpression = "0 12 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});*/

/***cron jobs de migracion***/
/*builder.Services.AddCronJob<MigrateEstudiantesCronJob>(options =>
{	
	options.CronExpression = "0 12 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});

builder.Services.AddCronJob<MigrateDocentesCronJob>(options =>
{	
	options.CronExpression = "0 12 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});

builder.Services.AddCronJob<MigrateGestionCursosCronJob>(options =>
{	
	options.CronExpression = "0 12 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});

builder.Services.AddCronJob<MigrateNotasCronJob>(options =>
{	
	options.CronExpression = "0 12 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});*/

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

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
