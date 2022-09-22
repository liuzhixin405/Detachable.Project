using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Detachable.Project.Core.Extensions;
using Detachable.Project.DependencyInjection;
using Detachable.Project.WebApi.Consistency;
using Detachable.Project.MessageBus;
using System.Collections.Generic;
using Detachable.Project;
using Detachable.Project.Abstractions;
using Detachable.Project.Publish;
using Detachable.Project.Entity.EventModel;
using Detachable.Project.WebApi.MessageBus.Consum;
using Detachable.Project.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFxServices();
builder.Services.AddAutoMapper();
builder.Host.UseDistributedLock();
builder.Host.UseCache();
builder.Services.AddHostedService<BackServices>();
builder.Services.AddControllers();
builder.Host.UseMessageBus(
    () => new List<IProducer>() { new Producer<Message<string>>(GlobalConstant.TopicTestName) ,new Producer<DictionaryMessageEvent>(GlobalConstant.DictionaryMessageEvent) }, 
    () => new List<IConsumer>() { new Consumer<Message<string>, MessageBusTestHandler>(GlobalConstant.TopicTestName) ,
    new Consumer<DictionaryMessageEvent,MessageDictionaryHandler>(GlobalConstant.DictionaryMessageEvent)});
builder.Services.AddHostedService<MessageBusProducerService>();

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
