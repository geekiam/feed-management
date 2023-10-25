using Api.Behaviours;
using Api.Middleware;
using FluentValidation;
using Geekiam.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Services;
using Threenine;
using Threenine.Data.DependencyInjection;
using Threenine.Services;

const string ConnectionsStringName = "Default";


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Api", Version = "v1"});
    c.CustomSchemaIds(x => x.FullName);
    c.DocumentFilter<JsonPatchDocumentFilter>();
    c.EnableAnnotations();
});
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddValidatorsFromAssemblies(new[] { typeof(Program).Assembly , typeof(FeedsDbContext).Assembly});
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
var connectionString = builder.Configuration.GetConnectionString(ConnectionsStringName);
builder.Services.AddDbContext<FeedsDbContext>(x => x.UseNpgsql(connectionString)).AddUnitOfWork<FeedsDbContext>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient(typeof(IEntityValidationService<>),typeof(EntityValidationService<>));
builder.Services.AddTransient(typeof(IDataService<>), typeof(DataService<>));
builder.Services.AddTransient<ISiteService, SiteService>();


var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Database migrations
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<FeedsDbContext>();
    context?.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}
app.UseHttpsRedirection();


app.UseAuthorization();
app.MapControllers();
app.Run();