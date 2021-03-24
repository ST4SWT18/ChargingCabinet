using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChargingCabinet.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace ChargingCarbinet.UnitTests
{
    public class TestLogFileSimulator
    {
        private LogFileSimulator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new LogFileSimulator();
        }

        [TestCase(50)]
        public void LogDoorLocked_WritesToFileWithID_LengthIsLargerThanOne(int id)
        {
            _uut.LogDoorLocked(id);
            var fileText = File.ReadLines("logfile.txt");
            Assert.IsTrue(fileText.ToString().Length > 1);
            
        }
    }


    public class AnotherTestLogFileSimulator
    {
        private LogFileSimulator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new LogFileSimulator();
        }

        [TestCase(50)]
        public void Test2(int id)
        {
            var Id = id;
            _uut.LogDoorUnlocked(Id);
            var fileText = File.ReadLines("logfile.txt");
            Assert.IsTrue(fileText.ToString().Length > 1);
        }
    }
}
