using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;
using NSubstitute;
using NUnit.Framework;

namespace ChargingCarbinet.Test
{
    public class Tests
    {
        private RfidReaderSimulator _uut;
        private RFIDDetectedEventArgs _rfidDetectedEventArgs;

        [SetUp]
        public void Setup()
        {
            _rfidDetectedEventArgs = null;
            _uut = new RfidReaderSimulator();

            _uut.RFIDDetectedEvent +=
                (o, args) =>
                {
                    _rfidDetectedEventArgs = args;
                };
        }

        [Test]
        public void RFIDDetectedValue_IsZero_ByDefault()
        {
            Assert.That(_uut.RFIDDetectedValue, Is.Zero);
        }

        [TestCase(5, 4)]
        public void RFIDDetectedValue_IsUpdated_WhenIDIsRead_FromUserInput(int oldId, int newId)
        {
            _uut.RFIDDetectedValue = oldId;
            _uut.OnRfidRead(newId);
            Assert.That(_uut.RFIDDetectedValue, Is.EqualTo(newId));
        }

        [Test]
        public void OnRfidRead_RFIDDetectedEventArgs_IsNull()
        {
            Assert.That(_rfidDetectedEventArgs, Is.Null);
        }

        [Test]
        public void OnRfidRead_RFIDDetectedEventArgs_IsNotNull()
        {
            _uut.OnRfidRead(1);
            Assert.Multiple(() =>
            {
                Assert.That(_rfidDetectedEventArgs, Is.Not.Null);
                //TODO Tjek at eventt indeholder 1
                Assert.That(_rfidDetectedEventArgs.RFIDDetected, Is.EqualTo(1));
            });

        }

    }
}