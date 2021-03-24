using System;
using ChargingCabinet.Events;

namespace ChargingCabinet.Interfaces
{
    public interface IDoorSimulator
    {
        event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        event EventHandler<DoorCloseEventArgs> DoorCloseEvent;
        public bool DoorOpenedValue { get; set; }
        public bool DoorClosedValue { get; set; }
        public void LockDoor();
        public void UnlockDoor();
    }
}