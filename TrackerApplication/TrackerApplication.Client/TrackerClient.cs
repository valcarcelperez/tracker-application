using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TrackerApplication.Contracts;
using TrackerApplication.Contracts.Models;

namespace TrackerApplication.Client
{
    public interface ITrackerClientLogger
    {
        void Info(string message);
        void Error(string message);
        void Warning(string message);
    }

    public class TrackerClientConfig
    {
        public TimeSpan RequestInterval { get; set; }
        public string BaseAddress { get; set; }
        public TimeSpan Timeout { get; set; }
    }

    public class TrackerDataReceivedEvenArgs : EventArgs
    {
        public TrackerData[] Data { get; set; }
    }

    public class TrackerClient : IDisposable
    {
        private readonly ITrackerClientLogger _logger;
        private readonly TrackerClientConfig _config;
        private Timer _timer = null;
        private readonly object _lockObj = new object();
        private readonly TrackerApiClient _trackerApiClient;
        private bool disposed = false;
        private bool sendingRequest = false;
        private JsonSerializerOptions _jsonSerializerOptions;

        public event EventHandler<TrackerDataReceivedEvenArgs> TrackerDataReceived;

        public TrackerClient(ITrackerClientLogger logger, TrackerClientConfig config)
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

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

        protected void OnTrackerDataReceived(TrackerDataReceivedEvenArgs e)
        {
            TrackerDataReceived?.Invoke(this, e);
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
            lock (_lockObj)
            {
                if (!sendingRequest)
                {
                    sendingRequest = true;
                }
                else
                {
                    return;
                }
            }

            _logger.Info("Sending request");

            try
            {
                var response = _trackerApiClient.RetrieveAllAsync().Result;
                var json = JsonSerializer.Serialize(response, _jsonSerializerOptions);
                
                if (response.Type == ServiceResponseType.Succeed)
                {
                    _logger.Info($"Response:{Environment.NewLine}{json}");
                    OnTrackerDataReceived(new TrackerDataReceivedEvenArgs { Data = response.Data });
                }
                else
                {
                    _logger.Warning($"Response:{Environment.NewLine}{json}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }            
            finally
            {
                lock (_lockObj)
                {
                    sendingRequest = false;
                }
            }
        }
    }
}
