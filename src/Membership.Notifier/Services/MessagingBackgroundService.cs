using Membership.Notifier.Messages;
using Membership.Shared.Dispatchers;
using Membership.Shared.Subscribers;


namespace Membership.Notifier.Services;

public class MessagingBackgroundService : BackgroundService
{
    private readonly IMessageSubscriber _messageSubscriber;
    private readonly IMessageDispatcher _dispatcher;
    private readonly ILogger<MessagingBackgroundService> _logger;

    public MessagingBackgroundService(IMessageSubscriber messageSubscriber, IMessageDispatcher dispatcher,
        ILogger<MessagingBackgroundService> logger)
    {
        _messageSubscriber = messageSubscriber;
        _dispatcher = dispatcher;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageSubscriber
            .SubscribeMessage<UserCreatedMessage>("user-created-notify-que", "#", "user",
                async (msg, args) =>
                {
                    _logger.LogInformation(
                        $"Received message: User Email {msg.Email} | Name: {msg.Name} | RoutingKey: {args.RoutingKey}");
                    await _dispatcher.DispatchAsync(msg);
                });
    }
}