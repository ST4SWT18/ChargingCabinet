using System.IO;

namespace ChargingCabinet.Interfaces
{
    public interface IWriteSimulator
    {
        string LockedMessage(int Id);
        string UnlockedMessage(int Id);
    }
}