
using FluentValidation.Results;

using MediatR;

using NYCSS.Infra.SqlServer.Interfaces;
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

        public Task<ValidationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid()) return request.ValidationResult;
            var cliente = new Models.Cliente(message.Id, message.Nome, message.Email, message.Cpf);
            var clienteExitente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);
            if (clienteExitente != null)
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }

            _clienteRepository.Adicionar(cliente);

            cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));
            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}
