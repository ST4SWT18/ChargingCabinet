using System;
using System.IO;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet.Simulators
{
    public class WriteSimulator : IWriteSimulator
    {
        public void WriteLineLocked(StreamWriter writer, int Id)
        {
            writer.WriteLine(LockedMessage(Id));
        }

        public void WriteLineUnlocked(StreamWriter writer, int Id)
        {
            writer.WriteLine(UnlockedMessage(Id));
        }

        public string LockedMessage(int Id)
        {
            return DateTime.Now + ": Skab låst med RFID: " + Id;
        }

        public string UnlockedMessage(int Id)
        {
            return DateTime.Now + ": Skab låst op med RFID: " + Id;
        }
    }
}