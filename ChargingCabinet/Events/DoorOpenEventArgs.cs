using System;

namespace ChargingCabinet.Events
{
    public class DoorOpenEventArgs : EventArgs
    {
        public bool DoorOpened{ get; set; }
    }
}