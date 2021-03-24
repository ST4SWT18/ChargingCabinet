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
        [Test]
        public void CheckIf_ShowConnectPhoneMessage_IsCalled_WhenDoorOpenEventIsTriggered()
        {

            _doorSimulator.DoorOpenEvent += Raise.EventWith(new DoorOpenEventArgs());

            _displaySimulator.Received(1).ShowConnectPhoneMessage();

        }        
        
        // Eventhandler der skal kalde metoden ShowReadRfidMessage
        [Test]
        public void CheckIf_ShowReadRfidMessage_IsCalled_WhenDoorCloseEventIsTriggered()
        {

            _doorSimulator.DoorCloseEvent += Raise.EventWith(new DoorCloseEventArgs());

            _displaySimulator.Received(1).ShowReadRfidMessage();

        }

        // ladeskabet er ledigt og lader er tilsluttet
        [Test]
        public void CheckIf_BehaviorIsRight_WhenLadeskabStateIsAvailable_AndChargerIsConnected()
        {
            int rfidDetected = 123;
            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(true);

            _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs(){RFIDDetected = rfidDetected});
            
            _doorSimulator.Received(1).LockDoor();
            _chargeControl.Received(1).StartCharge();
            _logFileSimulator.Received(1).LogDoorLocked(rfidDetected);
            _displaySimulator.Received(1).ShowPhoneChargingMessage();
            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Locked));

        }


        // ladeskabet er ledigt men lader er IKKE tilsluttet
        [Test]
        public void CheckIf_BehaviorIsRight_WhenLadeskabStateIsAvailable_AndChargerIsNotConnected()
        {
            int rfidDetected = 123;
            _uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(false);

            _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs(){RFIDDetected = rfidDetected});
            
            _doorSimulator.DidNotReceive().LockDoor();
            _chargeControl.DidNotReceive().StartCharge();
            _logFileSimulator.DidNotReceive().LogDoorLocked(rfidDetected);
            _displaySimulator.Received(1).ShowConnectionErrorMessage();
            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
        }        
        
        // Ladeskabet er åbent 
        [Test]
        public void CheckIf_BehaviorIsRight_WhenLadeskabStateIsDoorOpen()
        {
            int rfidDetected = 123;
            _uut.State = StationControl.LadeskabState.DoorOpen;

            _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs(){RFIDDetected = rfidDetected});
            
            _doorSimulator.DidNotReceive().LockDoor();
            _chargeControl.DidNotReceive().StartCharge();
            _logFileSimulator.DidNotReceive().LogDoorLocked(rfidDetected);
            _displaySimulator.DidNotReceive().ShowConnectionErrorMessage();
            Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
        }        
        
        //// Ladeskabet er låst 
        //[Test]
        //public void CheckIf_BehaviorIsRight_WhenLadeskabStateIsLocked()
        //{
        //    int rfidDetected = 123;
        //    _uut.State = StationControl.LadeskabState.Locked;

        //    _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs(){RFIDDetected = rfidDetected});
            
        //    _doorSimulator.DidNotReceive().LockDoor();
        //    _chargeControl.DidNotReceive().StartCharge();
        //    _logFileSimulator.DidNotReceive().LogDoorLocked(rfidDetected);
        //    _displaySimulator.DidNotReceive().ShowConnectionErrorMessage();
        //    _uut.Received(1).CheckId(_uut.OldId, rfidDetected);

        //}
    }
}