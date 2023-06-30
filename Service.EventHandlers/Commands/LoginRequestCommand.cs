using Domain.Models;
using MediatR;

namespace Service.EventHandlers.Commands
{
    public class LoginRequestCommand : IRequest<LoginResponse>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
