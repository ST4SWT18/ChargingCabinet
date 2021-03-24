using System;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet
{
    public class DoorSimulator : IDoorSimulator
    {
        public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        public event EventHandler<DoorCloseEventArgs> DoorCloseEvent;

        public bool IsLocked { get; set; } = true;

        public bool DoorOpenedValue { get; set; }
        public bool DoorClosedValue { get; set; }

        public DoorSimulator()
        {
            DoorOpenedValue = false;
            DoorClosedValue = true;
        }

        public void LockDoor()
        {
            IsLocked = true;
        }

        public void UnlockDoor()
        {
            IsLocked = false;
        }

        //Notify
        private void OnDoorOpen()
        {
            DoorOpenEvent?.Invoke(this, new DoorOpenEventArgs() {DoorOpened = DoorOpenedValue});
        }

        private void OnDoorClose()
        {
            DoorCloseEvent?.Invoke(this, new DoorCloseEventArgs() {DoorClosed = DoorClosedValue});
        }
    }
}