using Membership.Shared;

namespace Membership.Notifier.Messages;

public record UserCreatedMessage(string Name, string Email, string Otp) :  IMessage;