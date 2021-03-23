﻿using System;
using System.IO;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;

namespace Ladeskab
{
    public class StationControl : IStationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private IDisplaySimulator _displaySimulator;
        private ILogFileSimulator _logFileSimulator;
        private int _oldId;
        private IDoorSimulator _door;
        

        public StationControl(IDoorSimulator door, IChargeControl chargeControl, IDisplaySimulator displaySimulator, ILogFileSimulator logFileSimulator)
        {
            _door = door;
            _charger = chargeControl;
            _displaySimulator = displaySimulator;
            _logFileSimulator = logFileSimulator;

            _door.DoorOpenEvent += DoorOpened;
            _door.DoorCloseEvent += DoorClosed;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        _logFileSimulator.LogDoorLocked(_oldId);

                        _displaySimulator.ShowPhoneChargingMessage();
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _displaySimulator.ShowConnectionErrorMessage();
                    }

                    break;
                    //hvornår er DoorOpen casen relevant? 
                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    CheckId(_oldId, id);

                    break;
            }
        }

        public void DoorOpened(object sender, EventArgs e)
        {
            _displaySimulator.ShowConnectPhoneMessage();
        }

        public void DoorClosed(object sender, EventArgs e)
        {
            _displaySimulator.ShowReadRfidMessage();
            
        }

        public void CheckId(int OldId, int Id)
        {
            if (Id == _oldId)
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
