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

        public DoorSimulator()
        {
            DoorOpenedValue = false;
            DoorClosedValue = true;
        }

        public void LockDoor()
        {
            Console.WriteLine("Door has been locked");
            IsLocked = true; //tilføjet LB
        }

        public void UnlockDoor()
        {
            Console.WriteLine("Door has been unlocked");
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