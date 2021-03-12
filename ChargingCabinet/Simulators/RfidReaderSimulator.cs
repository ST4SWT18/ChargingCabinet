using System;
using ChargingCabinet.Events;
using Ladeskab;

namespace ChargingCabinet.Simulators
{
    public class RfidReaderSimulator
    {
        private IStationControl _stationControl;
        public event EventHandler<RFIDDetectedEventArgs> RFIDDetectedEvent;

        public RfidReaderSimulator(IStationControl stationControl)
        {
            _stationControl = stationControl;
        }

        public void OnRfidRead()
        {
            RFIDDetectedEvent?.Invoke(this, new RFIDDetectedEventArgs());
        }
    }
}