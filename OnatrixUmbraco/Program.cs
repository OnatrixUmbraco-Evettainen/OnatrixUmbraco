using Azure.Messaging.ServiceBus;
using OnatrixUmbraco.Helpers;
using OnatrixUmbraco.Services;
using Umbraco.Cms.Core.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
.Build();

builder.Services.AddTransient<Signature>();
builder.Services.AddTransient<IFormValidationService, FormValidationService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddSingleton(new ServiceBusClient(builder.Configuration.GetValue<string>("ServiceBus_Connection")));
builder.Services.AddScoped<FormSubmissionService>();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
