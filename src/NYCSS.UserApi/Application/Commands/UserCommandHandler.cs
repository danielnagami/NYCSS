using FluentValidation.Results;

using MediatR;

using NYCSS.Domain.Entities;
using NYCSS.Infra.SqlServer.Interfaces;
using NYCSS.UserApi.Application.Events;
using NYCSS.Utils.MessageBus.Messages;

namespace NYCSS.UserApi.Application.Commands
{
    public class UserCommandHandler : CommandHandler, IRequestHandler<RegisterUserCommand, ValidationResult>
    {
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid()) return request.ValidationResult;
            var user = new User(request.ID, request.Username, request.FirstName, request.LastName, request.Email, request.Age, request.Photo);
            var clienteExitente = await _userRepository.GetByUsername(request.Username);
            if (clienteExitente != null)
            {
                AddError("This username is already been used.");
                return ValidationResult;
            }

            _userRepository.Add(user);

            user.AddEvent(new UserRegisteredEvent(request.ID, request.FirstName, request.FirstName, request.LastName, request.Email, request.Age, request.Photo));
            return await PersistData(_userRepository.UnitOfWork);
        }
    }
}
