using Detachable.Project.WebApi.Extensions;
using System.Diagnostics;


/**********注意注意注意注意注意Detachable.Project.ExternalAssembly有可能不是最新的,需要删除再生成注意注意注意注意注意注意注意注意********************/
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddCache();
builder.Services.AddService();
builder.SetMessageBus();
builder.Services.AddControllers();
builder.Host.ConfigureLogging(log =>
{
    log.AddConsole();
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Detachable.Project.WebApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Detachable.Project.WebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
