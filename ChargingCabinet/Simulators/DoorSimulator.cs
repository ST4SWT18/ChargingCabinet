using System;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet
{
    public class DoorSimulator : IDoorSimulator
    {
        public event EventHandler<DoorOpenEventArgs> DoorOpenEvent;
        public event EventHandler<DoorCloseEventArgs> DoorCloseEvent;

        public bool DoorOpenedValue{ get; private set; }
        public bool DoorClosedValue{ get; private set; }

        public void LockDoor()
        {
            Console.WriteLine("Door has been locked");
        }

        public void UnlockDoor()
        {

            Console.WriteLine("Door has been unlocked");
        }

        //Notify
        public void OnDoorOpen()
        {
            DoorOpenEvent?.Invoke(this, new DoorOpenEventArgs() {DoorOpened = this.DoorOpenedValue});
        }

        public void OnDoorClose()
        {
            DoorCloseEvent?.Invoke(this, new DoorCloseEventArgs() {DoorClosed = this.DoorClosedValue});
        }
    }
}