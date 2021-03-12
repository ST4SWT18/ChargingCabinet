using System;
using ChargingCabinet.Events;

namespace ChargingCabinet
{
    public class DoorSimulator : IDoorSimulator
    {
        public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        public event EventHandler<DoorCloseEventArgs> DoorCloseEvent;

        public void LockDoor()
        {

        }

        public void UnlockDoor()
        {

        }

        public void OnDoorOpen()
        {

        }

        public void OnDoorClose()
        {

        }
    }
}