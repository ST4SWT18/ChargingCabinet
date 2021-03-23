using System;
using System.Collections.Generic;
using System.Text;
using ChargingCabinet.Interfaces;
using NUnit.Framework;

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

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
