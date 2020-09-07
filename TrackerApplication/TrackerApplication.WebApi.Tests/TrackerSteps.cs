using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Specflow.Steps.WebApi;
using System;
using TechTalk.SpecFlow;

namespace TrackerApplication.WebApi.Tests
{
    [Binding]
    [Scope(Feature = "Tracker")]
    public class TrackerSteps : WebApiSpecs
    {
        public const string BaseUrl = "http://localhost:5000";

        private static IWebHost _webHost;

        private static readonly WebApiSpecsConfig _config = new WebApiSpecsConfig { BaseUrl = BaseUrl };

        public TrackerSteps(TestContext testContext) : base(testContext, _config) { }

        [BeforeScenario]
        public void StartService()
        {
            _webHost = WebHost.CreateDefaultBuilder()
                .UseUrls(BaseUrl)
                .UseStartup<Startup>()
                .Build();

            _webHost.Start();
        }

        [AfterScenario]
        public void StopService()
        {
            _webHost.StopAsync(TimeSpan.FromSeconds(3)).Wait();
        }
    }
}
