using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TrackerApplication.Domain.TrackerDataFormat3;

namespace TrackerApplication.Domain.Tests
{
    [TestClass]
    public class TrackerDataFormat3NormalizerTest
    {
        private string _testFilesFolder;
        private TrackerData _trackerData;
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
                    CompanyId = "3",
                    CompanyName = "Foo3",
                    TrackerId = 1,
                    TrackerName = "XYZ-100",
                    FirstCrumbDtm = DateTime.Parse("2020-08-18 10:35:00"),
                    LastCrumbDtm = DateTime.Parse("2020-08-18 10:45:00"),
                    TempCount = 3,
                    AvgTemp = 33.15,
                    HumidityCount = 3,
                    AvgHumdity = 91.5
                },
                new Contracts.Models.TrackerData
                {
                    CompanyId = "3",
                    CompanyName = "Foo3",
                    TrackerId = 2,
                    TrackerName = "XYZ-200",
                    FirstCrumbDtm = DateTime.Parse("2020-08-19 10:35:00"),
                    LastCrumbDtm = DateTime.Parse("2020-08-19 10:45:00"),
                    TempCount = 3,
                    AvgTemp = 43.15,
                    HumidityCount = 3,
                    AvgHumdity = 92.5
                }
            };
        }

        [TestMethod]
        public void NormalizeTrackerDate()
        {
            var filename = Path.Combine(_testFilesFolder, "TrackerDataFoo3.json");
            _trackerData = DataLoader.Load<TrackerData>(filename);
            var actual = TrackerDataFormat3Normalizer.NormalizeTrackerData(_trackerData);

            actual.Should().BeEquivalentTo(_expectedTrackerDatas);
        }
    }
}
