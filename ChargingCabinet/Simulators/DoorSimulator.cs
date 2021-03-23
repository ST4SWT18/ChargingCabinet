using System;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet
{
    public class DoorSimulator : IDoorSimulator
    {
        public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        public event EventHandler<DoorCloseEventArgs> DoorCloseEvent;

        public bool IsLocked { get; set; } = true; //tilføjet LB

        public bool DoorOpenedValue { get; set; }
        public bool DoorClosedValue { get; set; }
        private IStationControl _stationControl;

        public DoorSimulator(IStationControl stationControl)
        {
            DoorOpenedValue = false;
            DoorClosedValue = true;
            _stationControl = stationControl;
        }

        public void LockDoor()
        {
            Console.WriteLine("Dør er låst");
            IsLocked = true; //tilføjet LB
        }

        public void UnlockDoor()
        {
            Console.WriteLine("Dør er ulåst");
            IsLocked = false; //tilføjet LB
        }

        //Notify
        public void OnDoorOpen()
        {
            DoorOpenEvent?.Invoke(this, new DoorOpenEventArgs() {DoorOpened = DoorOpenedValue});
        }

        public void OnDoorClose()
        {
            DoorCloseEvent?.Invoke(this, new DoorCloseEventArgs() {DoorClosed = DoorClosedValue});
        }
    }
}