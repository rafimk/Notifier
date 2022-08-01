using Membership.Notifier.DAL;
using Membership.Notifier.EmailService;
using Membership.Notifier.Services;
using Membership.Shared;
using Membership.Shared.Deduplication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddMessaging(builder.Configuration,c => c.AddDeduplication<NotifyDbContext>(builder.Configuration));
builder.Services.AddPostgres(builder.Configuration);
builder.Services.AddHostedService<MessagingBackgroundService>();

var app = builder.Build();

app.MapGet("/", () => "Notify Service!");

var scope = app.Services.CreateScope();
var ctx = scope.ServiceProvider.GetRequiredService<NotifyDbContext>();
ctx.Database.EnsureCreated();

app.Run();