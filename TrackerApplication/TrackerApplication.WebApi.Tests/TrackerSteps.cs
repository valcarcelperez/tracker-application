using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Specflow.Steps.WebApi;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using TrackerApplication.Client;
using TrackerApplication.Contracts;
using TrackerApplication.Domain;

namespace TrackerApplication.WebApi.Tests
{
    [Binding]
    [Scope(Feature = "Tracker")]
    public class TrackerSteps : WebApiSpecs
    {
        public const string BaseUrl = "http://localhost:5000";

        private TrackerApiClient _trackerApiClient;

        private IWebHost _webHost;
        private string _testFilesFolder;

        private static readonly WebApiSpecsConfig _config = new WebApiSpecsConfig { BaseUrl = BaseUrl };

        public TrackerSteps(TestContext testContext) : base(testContext, _config) 
        {
            _testFilesFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _testFilesFolder = Path.Combine(_testFilesFolder, "TestFiles");
            _trackerApiClient = new TrackerApiClient(new TrackerApiClientConfig { BaseAddress = BaseUrl, Timeout = TimeSpan.FromSeconds(10) });
        }

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

        [Given(@"I save format (1|2|3) tracker data from file '(.*)'")]
        public void SaveTrackerDataFromFile(int format, string filename)
        {
            var fullFilename = Path.Combine(_testFilesFolder, filename);

            if (!File.Exists(fullFilename))
            {
                Assert.Fail($"File '{filename}' not found");
            }

            ServiceResponse response = null;
            switch (format)
            {
                case 1:
                    var request1 = DataLoader.Load<Contracts.Models.TrackerDataFormat1.TrackerData1>(fullFilename);
                    response = _trackerApiClient.SaveTrackerData1Async(request1).Result;
                    break;
                case 2:
                    var request2 = DataLoader.Load<Contracts.Models.TrackerDataFormat2.TrackerData2>(fullFilename);
                    response = _trackerApiClient.SaveTrackerData2Async(request2).Result;
                    break;
                case 3:
                    var request3 = DataLoader.Load<Contracts.Models.TrackerDataFormat3.TrackerData3>(fullFilename);
                    response = _trackerApiClient.SaveTrackerData3Async(request3).Result;
                    break;
                default:
                    Assert.Fail("Invalid file format");
                    break;
            }

            Assert.AreEqual(ServiceResponseType.Succeed, response.Type);
        }

        [Given(@"content is loaded from file '(.*)'")]
        public void SaveTrackDataFromFile(string filename)
        {
            var fullFilename = Path.Combine(_testFilesFolder, filename);
            
            if (!File.Exists(fullFilename))
            {
                Assert.Fail($"File '{filename}' not found");
            }

            var content = File.ReadAllText(fullFilename);
            SetContent(content);            
        }
    }
}
