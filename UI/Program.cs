using AppGenerate;
using BackgroundJob.Cron.Jobs;
using CAPA_DATOS;
using CAPA_DATOS.Cron.Jobs;
using CAPA_NEGOCIO.Oparations;

//coneccion wilber
//SqlADOConexion.IniciarConexion("sa", "zaxscd", "localhost", "SIAC_CCA");
//MySQLConnection.IniciarConexion("root", "", "localhost", "siac_cca_production", 3306);

//coneccion alder
//SqlADOConexion.IniciarConexion("sa", "123", ".\\MSSQLSERVER3", "SIAC_CCA");
SqlADOConexion.IniciarConexion("sa", "123", ".\\SQLEXPRESS", "SIAC_CCA");
//MySQLConnection.IniciarConexion("root", "", "localhost", "siac_cca_production", 3306);
//SqlADOConexion.IniciarConexion("sa", "123", "localhost\\MSSQLSERVER01", "SIAC_CCA");

// coneccion cesar
//MySQLConnection.IniciarConexion("root", "", "localhost", "siac_cca_production", 3306);
//PostgresADOConexion.IniciarConexion("postgres", "zaxscd", "localhost", "pst", 5432);


AppGeneratorProgram.Main(); //generador de codigo

// Migraciones
/*new MigrateEstudiantes().Migrate();
new MigrateDocentes().Migrate();
new MigrateGestionCursos().Migrate();
new MigrateNotas().Migrate();*/


var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddRazorPages();

builder.Services.AddControllers().AddJsonOptions(JsonOptions =>
		JsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null);
		
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(40);
});
//CRONJOB

builder.Services.AddCronJob<DailyCronJob>(options =>
{
	// Corre cada minuto
	//options.CronExpression = "0 0 13 1/1 * ? *";//ejecucion diaria a las 1 de la mañana
	options.CronExpression = "0 12 * * *";
	options.TimeZone = TimeZoneInfo.Local;
});

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
