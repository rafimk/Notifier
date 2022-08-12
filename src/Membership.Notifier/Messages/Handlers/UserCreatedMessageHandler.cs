using Membership.Notifier.DAL;
using Membership.Notifier.DAL.Models;
using Membership.Notifier.EmailService;
using Membership.Notifier.Services;
using Membership.Shared;

namespace Membership.Notifier.Messages.Handlers;

internal sealed class UserCreatedMessageHandler : IMessageHandler<UserCreatedMessage>
{
    private readonly IEmailService _emailService;
    private readonly NotifyDbContext _dbContext;

    public UserCreatedMessageHandler(IEmailService emailService, NotifyDbContext dbContext)
    {
        _emailService = emailService;
        _dbContext = dbContext;
    }

    public async Task HandleAsync(UserCreatedMessage message)
    {
        var userCreated = new UserCreatedModel
        {
            Id = Guid.NewGuid(),
            Email = message.Email,
            Name = message.Name,
            Otp = message.Otp,
            IsSendEmail = false,
        };

        if (message.Email.Any())
        {
            var html = HtmlBinder.Create(message.Otp.Substring(0, 1),
                message.Otp.Substring(1, 1),
                message.Otp.Substring(2, 1),
                message.Otp.Substring(3, 1));
            await _emailService.Send(message.Email, "UAE KMCC User password", html);
            await _dbContext.UserCreated.AddAsync(userCreated);
        }
    }
}