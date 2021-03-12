namespace ChargingCabinet.Simulators
{
    public class LogFileSimulator : ILogFileSimulator
    {
        public void LogDoorLocked(int Id)
        {
            
        }

        public void LogDoorUnlocked(int Id)
        {

        }
    }

    public interface ILogFileSimulator
    {
        public void LogDoorLocked(int Id);

        public void LogDoorUnlocked(int Id);
    }
}