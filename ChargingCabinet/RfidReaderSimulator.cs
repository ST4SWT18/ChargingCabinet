using Ladeskab;

namespace ChargingCabinet
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
            _stationControl.
        }
    }
}