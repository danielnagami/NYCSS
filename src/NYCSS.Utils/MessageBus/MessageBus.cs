using EasyNetQ;
using EasyNetQ.Internals;

using NYCSS.Utils.MessageBus.Messages;

using Polly;

using RabbitMQ.Client.Exceptions;

namespace NYCSS.Utils.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus? _bus;
        private IAdvancedBus? _advancedBus;
        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }

        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;

        public IAdvancedBus AdvancedBus => _bus!.Advanced;

        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();

            _bus?.PubSub.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();

            await _bus.PubSub.PublishAsync(message);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();

            return _bus!.Rpc.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();

            try
            {
                return await _bus!.Rpc.RequestAsync<TRequest, TResponse>(request);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> reply)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();

            return _bus!.Rpc.Respond(reply);
        }

        public AwaitableDisposable<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> reply)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();

            return _bus!.Rpc.RespondAsync(reply);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();

            _bus!.PubSub.Subscribe(subscriptionId, onMessage);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            _bus!.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }

        private void TryConnect()
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_connectionString);
                _advancedBus = _bus.Advanced;
                _advancedBus.Disconnected += OnDisconnect;
            });
        }

        private void OnDisconnect(object s, EventArgs e)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }

        public void Dispose()
        {
            _bus!.Dispose();
        }
    }
}