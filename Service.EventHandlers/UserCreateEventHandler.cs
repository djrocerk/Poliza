using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;
using Service.EventHandlers.Commands;
using Service.EventHandlers.Responses;

namespace Service.EventHandlers
{
    public class UserCreateEventHandler : IRequestHandler<UserCreateCommand, UserInfoResult>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UserCreateEventHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UserInfoResult> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            await ValidateUser(request);

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = await _usuarioRepository.CreateAsync(_mapper.Map<Usuario>(request));

            return _mapper.Map<UserInfoResult>(user);
        }

        private async Task ValidateUser(UserCreateCommand user)
        {
            var old = await _usuarioRepository.GetOneAsync(m => m.UserName == user.UserName);
            if (old != null) throw new BadRequestException($"Ya existe un usuario ({user.UserName})");
        }
    }
}
