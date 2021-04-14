using System;
using System.IO;
using System.Security.Cryptography;
using ChargingCabinet.Simulators;

namespace ChargingCabinet.Interfaces
{
    public class LogFileSimulator : ILogFileSimulator
    {
        private string _logFile = "logfile.txt"; // Navnet på systemets log-fil
        private IWriteSimulator _write;

        public LogFileSimulator(IWriteSimulator write)
        {
            _write = write;
        }

        public void LogDoorLocked(int Id)
        {
            using (var writer = File.AppendText(_logFile))
            {
                writer.WriteLine(_write.LockedMessage(Id));
            }
        }

        public void LogDoorUnlocked(int Id)
        {
            using (var writer = File.AppendText(_logFile))
            {
                writer.WriteLine(_write.UnlockedMessage(Id));
            }
        }
    }
}