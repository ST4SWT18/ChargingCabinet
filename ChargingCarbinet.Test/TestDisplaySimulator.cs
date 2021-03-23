using System;
using System.Collections.Generic;
using System.Text;
using ChargingCabinet.Simulators;
using NUnit.Framework;

namespace ChargingCarbinet.UnitTests
{
    public class TestDisplaySimulator
    {
        private DisplaySimulator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new DisplaySimulator();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
