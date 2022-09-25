using Detachable.Project.WebApi.Extensions;
using System.Diagnostics;


/**********ע��ע��ע��ע��ע��Detachable.Project.ExternalAssembly�п��ܲ������µ�,��Ҫɾ��������ע��ע��ע��ע��ע��ע��ע��ע��********************/
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
