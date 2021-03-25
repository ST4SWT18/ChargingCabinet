namespace ChargingCabinet.Interfaces
{
    public interface IWriteSimulator
    {
        void WriteLineLocked(int Id);
        void WriteLineUnlocked(int Id);
    }
}