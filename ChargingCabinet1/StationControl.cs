using System;
using System.IO;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;

namespace ChargingCabinet
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private IChargeControl _charger;
        private IDisplaySimulator _displaySimulator;
        private ILogFileSimulator _logFileSimulator;
        private IDoorSimulator _door;
        private IRfidReaderSimulator _rfidReaderSimulator;

        public int OldId { get; set; }
        public LadeskabState State { get; set; }
        



        public StationControl(IDoorSimulator door, 
            IChargeControl chargeControl, 
            IDisplaySimulator displaySimulator, 
            ILogFileSimulator logFileSimulator, IRfidReaderSimulator rfidReaderSimulator)
        {
            _door = door;
            _charger = chargeControl;
            _displaySimulator = displaySimulator;
            _logFileSimulator = logFileSimulator;
            _rfidReaderSimulator = rfidReaderSimulator;

            _door.DoorOpenEvent += DoorOpened;
            _door.DoorCloseEvent += DoorClosed;
            _rfidReaderSimulator.RFIDDetectedEvent += RfidDetected;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(object sender, RFIDDetectedEventArgs e)
        {
            switch (State)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        OldId = e.RFIDDetected;
                        _logFileSimulator.LogDoorLocked(e.RFIDDetected);

                        _displaySimulator.ShowPhoneChargingMessage();
                        State = LadeskabState.Locked;
                    }
                    else
                    {
                        _displaySimulator.ShowConnectionErrorMessage();
                    }

                    break;
                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    CheckId(OldId, e.RFIDDetected);

                    break;
            }
        }

        private void DoorOpened(object sender, DoorOpenEventArgs e)
        {
            _displaySimulator.ShowConnectPhoneMessage();
        }

        private void DoorClosed(object sender, DoorCloseEventArgs e)
        {
            _displaySimulator.ShowReadRfidMessage();
        }

        public void CheckId(int OldId, int Id)
        {
            if (Id == OldId)
            {
                _charger.StopCharge();
                _door.UnlockDoor();
                _logFileSimulator.LogDoorUnlocked(Id);

                _displaySimulator.ShowTakePhoneAndCloseDoorMessage();
                State = LadeskabState.Available;
            }
            else
            {

                _displaySimulator.ShowRfidErrorMessage();
            }
        }
    }
}
