using System;
using ChargingCabinet.Events;

namespace ChargingCabinet.Interfaces
{
    public interface IDoorSimulator
    {
        event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        event EventHandler<DoorCloseEventArgs> DoorCloseEvent;
        public void LockDoor();
        public void UnlockDoor();
        public void OnDoorOpen();
        public void OnDoorClose();
    }
}