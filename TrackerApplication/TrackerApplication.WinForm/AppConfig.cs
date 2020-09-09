using Microsoft.Extensions.Configuration;
using TrackerApplication.Client;

namespace TrackerApplication.WinForm
{
    public class AppConfig
    {
        public TrackerClientConfig TrackerClientConfig { get; set; }

        public static AppConfig Load()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, false)
                .Build();

            var trackerClientConfig = configuration.GetSection("TrackerClient").Get<TrackerClientConfig>();

            return new AppConfig
            {
                TrackerClientConfig = trackerClientConfig
            };
        }
    }
}
