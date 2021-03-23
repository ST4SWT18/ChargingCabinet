using System;

namespace ChargingCabinet.Events
{
    public class RFIDDetectedEventArgs : EventArgs
    {
        public int RFIDDetected { get; set; }
    }
}