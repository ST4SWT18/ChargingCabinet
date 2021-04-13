using System.CodeDom.Compiler;
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

        ////Old ID og new ID er ens
        //[TestCase(25, 25)]
        //public void CheckIf_BehaviorIsRight_WhenOldId_IsEqualToNewId(int oldId, int id)
        //{
        //    //bool doorClose = true;
        //    //bool doorOpen = true;

        //    //_doorSimulator.DoorOpenEvent += Raise.EventWith(new DoorOpenEventArgs() { DoorOpened = doorOpen });
        //    //_doorSimulator.DoorCloseEvent += Raise.EventWith(new DoorCloseEventArgs() { DoorClosed = doorClose });
        //    //_rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFIDDetected = oldId});

        //    _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFIDDetected = id });

        //    _chargeControl.Received(1).StopCharge();
        //    _doorSimulator.Received(1).UnlockDoor();
        //    _logFileSimulator.Received(1).LogDoorUnlocked(id);
        //    _displaySimulator.Received(1).ShowTakePhoneAndCloseDoorMessage();
        //    //Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
        //}

        //Old ID og new ID er forskellige
        //[TestCase(25, 30)]
        //public void CheckIf_ShowRfidErrorMessage_IsCalled_WhenOldIdAndId_IsDifferent(int oldId, int id)
        //{

        //    //_uut.State = StationControl.LadeskabState.Locked;
        //    //_uut.OldId = oldId;
        //    //bool doorClose = true;
        //    //bool doorOpen = true;

        //    //_doorSimulator.DoorOpenEvent += Raise.EventWith(new DoorOpenEventArgs() { DoorOpened = doorOpen });
        //    //_doorSimulator.DoorCloseEvent += Raise.EventWith(new DoorCloseEventArgs() { DoorClosed = doorClose });
        //    //_rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFIDDetected = oldId });
        //    _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFIDDetected = id });

        //    _chargeControl.DidNotReceive().StopCharge();
        //    _doorSimulator.DidNotReceive().UnlockDoor();
        //    _logFileSimulator.DidNotReceive().LogDoorUnlocked(id);
        //    _displaySimulator.DidNotReceive().ShowTakePhoneAndCloseDoorMessage();
        //    _displaySimulator.Received(1).ShowRfidErrorMessage();
        //    //Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Locked));
        //}

        // Eventhandler der skal kalde metoden ShowConnectPhoneMessage
        [Test]
        public void CheckIf_ShowConnectPhoneMessage_IsCalled_WhenDoorOpenEventIsTriggered()
        {
            bool doorOpen = true;
            _doorSimulator.DoorOpenEvent += Raise.EventWith(new DoorOpenEventArgs() { DoorOpened = doorOpen });

            _displaySimulator.Received(1).ShowConnectPhoneMessage();

        }

        // Eventhandler der skal kalde metoden ShowReadRfidMessage
        [Test]
        public void CheckIf_ShowReadRfidMessage_IsCalled_WhenDoorCloseEventIsTriggered()
        {
            bool doorClose = true;
            _doorSimulator.DoorCloseEvent += Raise.EventWith(new DoorCloseEventArgs() { DoorClosed = doorClose });

            _displaySimulator.Received(1).ShowReadRfidMessage();

        }

        // ladeskabet er ledigt og lader er tilsluttet
        [TestCase(123)]
        public void CheckIf_BehaviorIsRight_WhenLadeskabStateIsAvailable_AndChargerIsConnected(int rfidId)
        {
            _chargeControl.IsConnected().Returns(true);

            _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFIDDetected = rfidId });

            _doorSimulator.Received(1).LockDoor();
            _chargeControl.Received(1).StartCharge();
            _logFileSimulator.Received(1).LogDoorLocked(rfidId);
            _displaySimulator.Received(1).ShowPhoneChargingMessage();
            //Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Locked));
        }


        // ladeskabet er ledigt men lader er IKKE tilsluttet
        [Test]
        public void CheckIf_BehaviorIsRight_WhenLadeskabStateIsAvailable_AndChargerIsNotConnected()
        {
            int rfidDetected = 123;
            //_uut.State = StationControl.LadeskabState.Available;
            _chargeControl.IsConnected().Returns(false);

            _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFIDDetected = rfidDetected });

            _doorSimulator.DidNotReceive().LockDoor();
            _chargeControl.DidNotReceive().StartCharge();
            _logFileSimulator.DidNotReceive().LogDoorLocked(rfidDetected);
            _displaySimulator.Received(1).ShowConnectionErrorMessage();
            //Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.Available));
        }

        // Ladeskabet er åbent 
        //[Test]
        //public void CheckIf_BehaviorIsRight_WhenLadeskabStateIsDoorOpen()
        //{
        //    int rfidDetected = 123;
        //    //_uut.State = StationControl.LadeskabState.DoorOpen;

        //    _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFIDDetected = rfidDetected });

        //    _doorSimulator.DidNotReceive().LockDoor();
        //    _chargeControl.DidNotReceive().StartCharge();
        //    _logFileSimulator.DidNotReceive().LogDoorLocked(rfidDetected);
        //    _displaySimulator.DidNotReceive().ShowConnectionErrorMessage();
        //    //Assert.That(_uut.State, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
        //}

        //// Ladeskabet er låst 
        //[Test]
        //public void CheckIf_BehaviorIsRight_WhenLadeskabStateIsLocked()
        //{
        //    int rfidDetected = 123;
        //    //_uut.State = StationControl.LadeskabState.Locked;

        //    _rfidReaderSimulator.RFIDDetectedEvent += Raise.EventWith(new RFIDDetectedEventArgs() { RFIDDetected = rfidDetected });

        //    _doorSimulator.DidNotReceive().LockDoor();
        //    _chargeControl.DidNotReceive().StartCharge();
        //    _logFileSimulator.DidNotReceive().LogDoorLocked(rfidDetected);
        //    _displaySimulator.DidNotReceive().ShowConnectionErrorMessage();

        //}
    }
}