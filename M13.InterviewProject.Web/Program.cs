using FluentValidation.AspNetCore;
using M13.InterviewProject.Application;
using M13.InterviewProject.Application.Consumers.Rule.Commands.CreateRule;
using M13.InterviewProject.Web.Extensions;
using M13.InterviewProject.Web.Filters;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var services = builder.Services;
// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMediator();
// builder.Services.AddDistributedMemoryCache();
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetSection("RedisConnection").Value;
});

services.AddMvc(options =>
{
    options.Filters.Add(new ValidationFilter());
});
services
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRuleCommandValidator>());
services.AddFluentValidationRulesToSwagger();

services.AddApplication();
services.AddAutoMapper();


var app = builder.Build();

app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();