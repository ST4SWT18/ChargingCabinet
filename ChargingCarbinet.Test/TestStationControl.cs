using ChargingCabinet;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;
using NSubstitute;
using NUnit.Framework;

namespace ChargingCarbinet.UnitTests
{
    public class TestStationControl
    {
        private IDoorSimulator _doorSimulator;
        private IChargeControl _chargeControl;
        private IDisplaySimulator _displaySimulator;
        private ILogFileSimulator _logFileSimulator;
        private IRfidReaderSimulator _rfidReaderSimulator;
        private StationControl _uut;

        [SetUp]
        public void Setup()
        {
            _doorSimulator = Substitute.For<IDoorSimulator>();
            _chargeControl = Substitute.For<IChargeControl>();
            _displaySimulator = Substitute.For<IDisplaySimulator>();
            _logFileSimulator = Substitute.For<ILogFileSimulator>();
            _rfidReaderSimulator = Substitute.For<IRfidReaderSimulator>();
            _uut = new StationControl(_doorSimulator, _chargeControl, _displaySimulator, _logFileSimulator, _rfidReaderSimulator);
        }

        [TestCase(25, 25)]
        public void Test1(int oldId, int id)
        {
            Assert.That(oldId, Is.EqualTo(id));
        }

        [TestCase(25, 25)]
        public void Test2(int oldId, int id)
        {
            _uut.CheckId(oldId, id);
            _chargeControl.Received(1).StopCharge();
            _doorSimulator.Received(1).UnlockDoor();
            _logFileSimulator.Received(1).LogDoorUnlocked(id);
            _displaySimulator.Received(1).ShowTakePhoneAndCloseDoorMessage();
        }

        [TestCase(25, 30)]
        public void Test3(int oldId, int id)
        {
            _uut.CheckId(oldId, id);
            _displaySimulator.Received(1).ShowRfidErrorMessage();
        }

        // Eventhandler der skal kalde metoden ShowConnectPhoneMessage
        [TestCase(0.0001)]
        public void CheckIf_ShowFullyChargedMessage_IsCalled_WhenCurrentCurrentIsHigherThan0AndEqualToOrLessThan5(double currentCurrent)
        {
            _uut. += Raise.EventWith(new CurrentEventArgs() { Current = _uut.CurrentCurrent });
            _displaySimulator.Received(1).ShowFullyChargedMessage();

        }
    }
}