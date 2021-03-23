using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;
using NSubstitute;
using NUnit.Framework;

namespace ChargingCarbinet.Test
{
    public class Tests
    {
        private IStationControl _stationControl;
        private RfidReaderSimulator _uut;
        [SetUp]
        public void Setup()
        {
            _stationControl = Substitute.For<IStationControl>();
            _uut = new RfidReaderSimulator(_stationControl);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}