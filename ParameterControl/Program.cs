using ParameterControl.Services.Results;
using ParameterControl.Services.Indicators;
using ParameterControl.Services.Users;
using ParameterControl.Services.Scenarios;
using ParameterControl.Services.Parameters;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Rows;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IPoliciesServices, PoliciesServices>();
builder.Services.AddTransient<IConciliationsServices, ConciliationsServices>();
builder.Services.AddTransient<IParametersService, ParametersService>();
builder.Services.AddTransient<IScenariosServices, ScenariosServices>();
builder.Services.AddTransient<IUsersServices, UsersServices>();
builder.Services.AddTransient<IIndicatorsService, IndicatorsService>();
builder.Services.AddTransient<IResultsServices, ResultsServices>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<Rows>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
