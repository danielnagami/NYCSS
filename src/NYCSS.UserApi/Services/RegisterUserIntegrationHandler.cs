using FluentValidation.Results;

using NYCSS.UserApi.Application.Commands;
using NYCSS.Utils.Mediator;
using NYCSS.Utils.MessageBus;
using NYCSS.Utils.MessageBus.Messages;

namespace NYCSS.UserApi.Services
{
    public class RegisterUserIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegisterUserIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetRespond();

            return Task.CompletedTask;
        }

        private void SetRespond()
        {
            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
              await RegisterUser(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetRespond();
        }

        public async Task<ResponseMessage> RegisterUser(UserRegisteredIntegrationEvent message)
        {
            var clienteCommand = new RegisterUserCommand(message.ID, message.Username, message.FirstName, message.LastName, message.Email, message.Age, message.Photo);

            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.SendCommand(clienteCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}