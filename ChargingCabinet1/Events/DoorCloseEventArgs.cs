using System;

namespace ChargingCabinet.Events
{
    public class DoorCloseEventArgs : EventArgs
    {
        public bool DoorClosed{ get; set; }
    }
}