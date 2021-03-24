using System;
using System.IO;
using ChargingCabinet.Simulators;

namespace ChargingCabinet.Interfaces
{
    public class LogFileSimulator : ILogFileSimulator
    {
        private string logFile1 = "logfile1.txt"; // Navnet på systemets log-fil
        private string logFile2 = "logfile2.txt"; // Navnet på systemets log-fil

        public void LogDoorLocked(int Id)
        {
            using (var writer = File.AppendText(logFile1))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", Id);
            }
        }

        public void LogDoorUnlocked(int Id)
        {
            using (var writer = File.AppendText(logFile2))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", Id);
            }
        }
    }
}