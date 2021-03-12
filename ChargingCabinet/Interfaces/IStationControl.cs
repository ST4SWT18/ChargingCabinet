namespace ChargingCabinet.Interfaces
{
    public interface IStationControl
    {
        public void DoorOpened();
        public void DoorClosed();
        public void CheckId(int OldId, int Id);
    }
}