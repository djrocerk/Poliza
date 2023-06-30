using MediatR;
using Service.EventHandlers.Responses;
using System.ComponentModel.DataAnnotations;

namespace Service.EventHandlers.Commands
{
    public class UserCreateCommand : IRequest<UserInfoResult>
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
    }
}
