using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BoardGame.Api.BackgroundServices
{
    public abstract class TurnBackgroundService : IHostedService
    {
        private readonly ILogger<TurnBackgroundService> _logger;
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(EndCurrentTurnAsync, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        protected abstract void EndCurrentTurnAsync(object state);
    }
}
