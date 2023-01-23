using NLog.Web;
using WeatherApp;
using WeatherApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDataService, DataServices>();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var connectionValues = new ConnectionValues();
builder.Configuration.GetSection("ConnectionValues").Bind(connectionValues);
builder.Services.AddSingleton(connectionValues);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WeatherApp}/{action=SearchByCity}");

app.Run();
