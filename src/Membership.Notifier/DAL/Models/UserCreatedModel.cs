using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Membership.Notifier.DAL.Models;

public class UserCreatedModel
{
    [Key]
    public Guid Id { get; set; }
    [AllowNull]
    public string Email { get; set; }
    [AllowNull]
    public string Name { get; set; }
    [AllowNull]
    public string Otp { get; set; }
    public bool IsSendEmail { get; set; }
}