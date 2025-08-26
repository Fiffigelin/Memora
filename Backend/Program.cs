using Backend.Startup;
using Memora.Backend.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.AddDependencies();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerServices();

var app = builder.Build();

app.ApplyCorsConfig();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSwaggerUI();
app.MapControllers();

app.Run();