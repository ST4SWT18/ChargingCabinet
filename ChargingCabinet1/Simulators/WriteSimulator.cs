using System;
using System.IO;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet.Simulators
{
    public class WriteSimulator : IWriteSimulator
    {
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