using System;
using ChargingCabinet.Events;

namespace ChargingCabinet.Interfaces
{
    public interface IRfidReaderSimulator
    {
        event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;

        void OnRfidRead();
    }
}