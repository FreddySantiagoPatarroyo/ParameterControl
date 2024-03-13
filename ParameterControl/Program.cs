using Microsoft.AspNetCore.Authentication.Cookies;
using ParameterControl.Services.ApprovedResults;
using ParameterControl.Services.Audit;
using ParameterControl.Services.Authenticated;
using ParameterControl.Services.ConciliationExecition;
using ParameterControl.Services.ConciliationExecution;
using ParameterControl.Services.Conciliations;
using ParameterControl.Services.CrossConnections;
using ParameterControl.Services.Indicators;
using ParameterControl.Services.Parameters;
using ParameterControl.Services.Policies;
using ParameterControl.Services.Results;
using ParameterControl.Services.Rows;
using ParameterControl.Services.Scenarios;
using ParameterControl.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IPoliciesServices, PoliciesServices>();
builder.Services.AddTransient<IConciliationsServices, ConciliationsServices>();
builder.Services.AddTransient<IParametersService, ParametersService>();
builder.Services.AddTransient<IScenariosServices, ScenariosServices>();
builder.Services.AddTransient<IUsersServices, UsersServices>();
builder.Services.AddTransient<IIndicatorsService, IndicatorsService>();
builder.Services.AddTransient<IResultsServices, ResultsServices>();
builder.Services.AddTransient<ICrossConnectionsService, CrossConnectionsService>();
builder.Services.AddTransient<IConciliationExecutionService, ConciliationExecutionService>();
builder.Services.AddTransient<IApprovedResultsServices, ApprovedResultsServices>();
builder.Services.AddTransient<IAuditsService, AuditsService>();
builder.Services.AddTransient<AuthenticatedUser>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<Rows>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Login/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        option.AccessDeniedPath = "/Home/AuthorizedError";
    });

builder.Services.AddHttpContextAccessor();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
