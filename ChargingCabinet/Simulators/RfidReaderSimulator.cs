using System;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;
using Ladeskab;

namespace ChargingCabinet.Simulators
{
    public class RfidReaderSimulator : IRfidReaderSimulator
    {
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;
        public bool RFIDDetectedValue { get; private set; }

        public RfidReaderSimulator()
        {
        }

        public void OnRfidRead(int id)
        {
            RFIDDetectedEvent?.Invoke(this, new RFIDDetectedEventArgs() {RFIDDetected = this.RFIDDetectedValue});
        }
    }
}