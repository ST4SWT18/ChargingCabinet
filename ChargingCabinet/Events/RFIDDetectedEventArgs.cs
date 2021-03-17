using System;

namespace ChargingCabinet.Events
{
    public class RFIDDetectedEventArgs : EventArgs
    {
        public bool RFIDDetected { get; set; }
    }
}