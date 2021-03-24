using System.IO;
using ChargingCabinet.Interfaces;
using NUnit.Framework;

namespace ChargingCarbinet.UnitTests
{
    public class TestLogFileSimulatorUnlocked
    {
        private LogFileSimulator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new LogFileSimulator();
        }

        //[TestCase(50)]
        //public void Test2(int id)
        //{
        //    var Id = id;
        //    _uut.LogDoorUnlocked(Id);
        //    var fileText = File.ReadLines("logfile.txt");
        //    Assert.IsTrue(fileText.ToString().Length > 1);
        //}
    }
}