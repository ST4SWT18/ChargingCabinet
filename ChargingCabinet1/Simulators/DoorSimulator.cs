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
        public void OnDoorOpen(bool doorOpen)
        {
            DoorOpenedValue = doorOpen;
            DoorOpenEvent?.Invoke(this, new DoorOpenEventArgs() {DoorOpened = DoorOpenedValue});
        }

        public void OnDoorClose(bool doorClose)
        {
            DoorClosedValue = doorClose;
            DoorCloseEvent?.Invoke(this, new DoorCloseEventArgs() {DoorClosed = DoorClosedValue});
        }
    }
}