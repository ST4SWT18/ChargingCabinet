using System;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet.Simulators
{
    public class RfidReaderSimulator : IRfidReaderSimulator
    {
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;
        public int RFIDDetectedValue { get; set; }

        public RfidReaderSimulator()
        {
        }

        public void OnRfidRead(int id)
        {
            RFIDDetectedValue = id;
            RFIDDetectedEvent?.Invoke(this, new RFIDDetectedEventArgs() {RFIDDetected = RFIDDetectedValue});
        }
    }
}