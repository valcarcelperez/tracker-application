using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using FluentAssertions;
using System.Collections.Generic;

namespace TrackerApplication.Domain.Tests
{
    [TestClass]
    public class TrackerDataNormalizerFormat1Test
    {
        private string _testFilesFolder;
        private TrackerDataFormat1.TrackerData _trackerData;
        private NormalizedTrackerData.TrackerData _expectedTrackerData;

        [TestInitialize]
        public void Initialize()
        {
            _testFilesFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _testFilesFolder = Path.Combine(_testFilesFolder, "TestFiles");
            

            _expectedTrackerData = new NormalizedTrackerData.TrackerData
            {
                CompanyId = "1",
                CompanyName = "Foo1",
                Trackers = new List<NormalizedTrackerData.Tracker>()
            };

            _expectedTrackerData.Trackers.Add(new NormalizedTrackerData.Tracker { TrackerId = 1, TrackerName = "ABC-100" });
            _expectedTrackerData.Trackers.Add(new NormalizedTrackerData.Tracker { TrackerId = 2, TrackerName = "ABC-200" });
        }

        [TestMethod]
        public void NormalizeTrackerDate()
        {
            var filename = Path.Combine(_testFilesFolder, "TrackerDataFoo1.json");
            _trackerData = DataLoader.Load<TrackerDataFormat1.TrackerData>(filename);
            var actual = TrackerDataNormalizer.NormalizeTrackerData(_trackerData);

            actual.Should().BeEquivalentTo(_expectedTrackerData);
        }
    }
}
