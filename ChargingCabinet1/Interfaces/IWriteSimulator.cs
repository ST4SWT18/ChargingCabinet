using System.IO;

namespace ChargingCabinet.Interfaces
{
    public interface IWriteSimulator
    {
        //void WriteLineLocked(StreamWriter writer, int Id);
        //void WriteLineUnlocked(StreamWriter writer, int Id);
        string LockedMessage(int Id);
        string UnlockedMessage(int Id);
    }
}