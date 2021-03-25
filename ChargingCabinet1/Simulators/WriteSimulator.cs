using System;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet.Simulators
{
    public class WriteSimulator : IWriteSimulator
    {
        public void WriteLineLocked(int Id)
        {
            Console.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", Id);
        }

        public void WriteLineUnlocked(int Id)
        {
            Console.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", Id);
        }
    }
}