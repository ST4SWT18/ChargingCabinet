namespace ChargingCabinet
{
    public interface IDoorSimulator
    {
        public void LockDoor();
        public void UnlockDoor();
        public void OnDoorOpen();
        public void OnDoorClose();
    }
}