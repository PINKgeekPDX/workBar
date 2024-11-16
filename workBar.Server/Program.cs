using workBar.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your services
builder.Services.AddScoped<IButtonService, ButtonService>();
builder.Services.AddScoped<IScriptService, ScriptService>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Add Swagger if needed
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
