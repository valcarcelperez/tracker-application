using System;
using System.Threading;
using System.Threading.Tasks;

namespace TrackerApplication.Client
{
    public interface ITrackerClientLogger
    {
        void Info(string message);
        void Error(string message);
    }

    public class TrackerClientConfig
    {
        public TimeSpan RequestInterval { get; set; }
        public string BaseAddress { get; set; }
        public TimeSpan Timeout { get; set; }

    }

    public class TrackerClient : IDisposable
    {
        private readonly ITrackerClientLogger _logger;
        private readonly TrackerClientConfig _config;
        private Timer _timer = null;
        private readonly object _lockObj = new object();
        private readonly TrackerApiClient _trackerApiClient;
        private bool disposed = false;

        public TrackerClient(ITrackerClientLogger logger, TrackerClientConfig config)
        {
            _logger = logger;
            _config = config;

            _trackerApiClient = CreateTrackerApiClient();
        }

        ~TrackerClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _trackerApiClient.Dispose();
                }

                disposed = true;
            }
        }

        private TrackerApiClient CreateTrackerApiClient()
        {
            var trackerApiClientConfig = new TrackerApiClientConfig
            {
                BaseAddress = _config.BaseAddress,
                Timeout = _config.Timeout
            };

            return new TrackerApiClient(trackerApiClientConfig);
        }

        public void Start()
        {
            lock(_lockObj)
            {
                if (_timer != null)
                {
                    return;
                }

                UnsafeInitializeTimer();
                Task.Run(() => _logger.Info($"Started"));
            }
        }

        public void Stop()
        {
            lock (_lockObj)
            {
                if (_timer == null)
                {
                    return;
                }

                UnsafeCleanupTimer();
                Task.Run(() => _logger.Info($"Stopped"));
            }
        }

        private void UnsafeInitializeTimer()
        {
            var period = (int)_config.RequestInterval.TotalMilliseconds;
            _timer = new Timer(TimerCallback, null, period, period);
        }

        private void UnsafeCleanupTimer()
        {
            _timer?.Dispose();
            _timer = null;
        }

        private void TimerCallback(object state)
        {
            _logger.Info($"TimerCallback");
        }
    }
}
