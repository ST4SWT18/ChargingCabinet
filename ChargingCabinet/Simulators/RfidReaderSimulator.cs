using System;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;
using Ladeskab;

namespace ChargingCabinet.Simulators
{
    public class RfidReaderSimulator : IRfidReaderSimulator
    {
        private IStationControl _stationControl;
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;
        public bool RFIDDetectedValue { get; private set; }

        public RfidReaderSimulator(IStationControl stationControl)
        {
            _stationControl = stationControl;
        }

        public void OnRfidRead()
        {
            RFIDDetectedEvent?.Invoke(this, new RFIDDetectedEventArgs() {RFIDDetected = this.RFIDDetectedValue});
        }
    }
}