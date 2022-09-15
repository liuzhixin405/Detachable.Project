using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using My.Project.Core.Extensions;
using My.Project.DependencyInjection;
using My.Project.WebApi.Consistency;
using My.Project.MessageBus;
using System.Collections.Generic;
using My.Project;
using My.Project.Abstractions;
using My.Project.Publish;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFxServices();
builder.Services.AddAutoMapper();
builder.Host.UseDistributedLock();
builder.Host.UseCache();
builder.Services.AddHostedService<BackServices>();
builder.Services.AddControllers();
builder.Host.UseMessageBus(() => new List<IProducer>() { new Producer<Message<string>>("TopicTestName") }, () => new List<IConsumer>() { new Consumer<Message<string>, MessageBusTestHandler>("TopicTestName") });
builder.Services.AddHostedService<MessageBusProducerService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My.Project.WebApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My.Project.WebApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
