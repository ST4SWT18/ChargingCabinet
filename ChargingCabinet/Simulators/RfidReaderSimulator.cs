using Ladeskab;

namespace ChargingCabinet.Simulators
{
    public class RfidReaderSimulator
    {
        private StationControl _stationControl;

        public RfidReaderSimulator(StationControl stationControl)
        {
            _stationControl = stationControl;
        }

        public void OnRfidRead(int Id)
        {
        }
    }
}