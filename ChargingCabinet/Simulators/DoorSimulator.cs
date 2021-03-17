using System;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;

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

        //Notify
        public void OnDoorOpen()
        {

        }

        public void OnDoorClose()
        {

        }
    }
}