using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace TrackerApplication.Domain.Tests
{
    [TestClass]
    public class TrackerDataNormalizerFormat1Test
    {
        private string _testFilesFolder;
        private TrackerDataFormat1.TrackerData _trackerData;
        private NormalizedTrackerData.TrackerData _normalizedTrackerData;

        [TestInitialize]
        public void Initialize()
        {
            _testFilesFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _testFilesFolder = Path.Combine(_testFilesFolder, "TestFiles");
            var filename = Path.Combine(_testFilesFolder, "TrackerDataFoo1.json");
            _trackerData = DataLoader.Load<TrackerDataFormat1.TrackerData>(filename);
            _normalizedTrackerData = TrackerDataNormalizer.NormalizeTrackerDate(_trackerData);
        }

        [TestMethod]
        public void NormalizeTrackerDate_Given_format1_data_CompanyId_should_be_the_value_of_partnerId()
        {
            Assert.AreEqual("1", _normalizedTrackerData.CompanyId);
        }

        [TestMethod]
        public void NormalizeTrackerDate_Given_format1_data_CompanyName_should_be_the_value_of_partnerName()
        {
            Assert.AreEqual("Foo1", _normalizedTrackerData.CompanyName);
        }
    }
}
