using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using FluentAssertions;
using System.Collections.Generic;
using System;
using TrackerApplication.Contracts.Models.TrackerDataFormat1;

namespace TrackerApplication.Domain.Tests
{
    [TestClass]
    public class TrackerDataFormat1NormalizerTest
    {
        private string _testFilesFolder;
        private TrackerData1 _trackerData;
        private IEnumerable<Contracts.Models.TrackerData> _expectedTrackerDatas;

        [TestInitialize]
        public void Initialize()
        {
            _testFilesFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _testFilesFolder = Path.Combine(_testFilesFolder, "TestFiles");

            _expectedTrackerDatas = new List<Contracts.Models.TrackerData>
            {
                new Contracts.Models.TrackerData
                {
                    CompanyId = "1",
                    CompanyName = "Foo1",
                    TrackerId = 1,
                    TrackerName = "ABC-100",
                    FirstCrumbDtm = DateTime.Parse("2020-08-17 10:35:00"),
                    LastCrumbDtm = DateTime.Parse("2020-08-17 10:45:00"),
                    TempCount = 3,
                    AvgTemp = 23.15,
                    HumidityCount = 3,
                    AvgHumdity = 81.5
                },
                new Contracts.Models.TrackerData
                {
                    CompanyId = "1",
                    CompanyName = "Foo1",
                    TrackerId = 2,
                    TrackerName = "ABC-200",
                    FirstCrumbDtm = DateTime.Parse("2020-08-18 10:35:00"),
                    LastCrumbDtm = DateTime.Parse("2020-08-18 10:45:00"),
                    TempCount = 3,
                    AvgTemp = 24.15,
                    HumidityCount = 3,
                    AvgHumdity = 82.5
                }
            };
        }

        [TestMethod]
        public void NormalizeTrackerDate()
        {
            var filename = Path.Combine(_testFilesFolder, "TrackerDataFoo1.json");
            _trackerData = DataLoader.Load<TrackerData1>(filename);
            var actual = TrackerDataFormat1Normalizer.NormalizeTrackerData(_trackerData);

            actual.Should().BeEquivalentTo(_expectedTrackerDatas);
        }
    }
}
