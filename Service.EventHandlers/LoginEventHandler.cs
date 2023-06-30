using Domain.Entities;
using Domain.Exceptions;
using Domain.Jwt;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using Service.EventHandlers.Commands;

namespace Service.EventHandlers
{
    public class LoginEventHandler : IRequestHandler<LoginRequestCommand, LoginResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtFactory _jwtFactory;

        public LoginEventHandler(IUsuarioRepository usuarioRepository, IJwtFactory jwtFactory)
        {
            _usuarioRepository = usuarioRepository;
            _jwtFactory = jwtFactory;
        }

        public async Task<LoginResponse> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {
            Usuario user = await _usuarioRepository.GetOneAsync(m => m.UserName == request.UserName);
            if (user == null) throw new BadRequestException("Usuario y/o contraseña incorrectos");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new BadRequestException("Usuario y/o contraseña incorrectos");

            return _jwtFactory.CreateJwtToken(user);
        }
    }
}
