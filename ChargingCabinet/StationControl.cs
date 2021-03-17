using System;
using System.IO;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;

namespace Ladeskab
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
                        CheckId(_oldId,id);
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _logFileSimulator.LogDoorUnlocked(id);

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        public void DoorOpened()
        {
            Console.WriteLine("Tilslut telefon");
        }

        public void DoorClosed()
        {
            Console.WriteLine("Indlæs RFID");
        }

        public void CheckId(int OldId, int Id)
        {
            if (OldId == Id)
            {
                _door.LockDoor();
                _charger.StartCharge();
                _oldId = Id;
                _logFileSimulator.LogDoorLocked(Id);

                Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                _state = LadeskabState.Locked;
            }
            else
            {
                Console.WriteLine("Forkert RFID tag");
            }
        }

        // Her mangler de andre trigger handlere
    }
}
