using Vime.Server.Common.Hubs;
using Vime.Server.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);
builder.Services.AddFeaturesServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<SignalrJwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ApplicationHub>("/hub");

app.Run();

