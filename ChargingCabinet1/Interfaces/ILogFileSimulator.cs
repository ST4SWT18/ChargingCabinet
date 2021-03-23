namespace ChargingCabinet.Simulators
{
    public interface ILogFileSimulator
    {
        public void LogDoorLocked(int Id);

        public void LogDoorUnlocked(int Id);
    }
}