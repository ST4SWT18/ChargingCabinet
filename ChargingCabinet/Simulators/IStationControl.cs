namespace ChargingCabinet.Simulators
{
    public interface IStationControl
    {
        public void DoorOpened();
        public void DoorClosed();
        public void CheckId(int OldId, int Id);
    }
}