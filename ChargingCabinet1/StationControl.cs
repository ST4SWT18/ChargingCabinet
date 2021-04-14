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
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private IChargeControl _charger;
        private IDisplaySimulator _displaySimulator;
        private ILogFileSimulator _logFileSimulator;
        private IDoorSimulator _door;
        private IRfidReaderSimulator _rfidReaderSimulator;

        private int _oldId;
        private LadeskabState _state;


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
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = e.RFIDDetected;
                        _logFileSimulator.LogDoorLocked(e.RFIDDetected);

                        _displaySimulator.ShowPhoneChargingMessage();
                        _state = LadeskabState.Locked;
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
                    CheckId(_oldId, e.RFIDDetected);
                    break;
            }
        }

        private void DoorOpened(object sender, DoorOpenEventArgs e)
        {
            _state = LadeskabState.DoorOpen;

            _displaySimulator.ShowConnectPhoneMessage();
        }

        private void DoorClosed(object sender, DoorCloseEventArgs e)
        {
            _state = LadeskabState.Available;

            _displaySimulator.ShowReadRfidMessage();
        }

        private void CheckId(int OldId, int Id)
        {
            if (Id == OldId)
            {
                _charger.StopCharge();
                _door.UnlockDoor();
                _logFileSimulator.LogDoorUnlocked(Id);

                _displaySimulator.ShowTakePhoneAndCloseDoorMessage();
                _state = LadeskabState.Available;
            }
            else
            {

                _displaySimulator.ShowRfidErrorMessage();
            }
        }
    }
}
