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
        private StreamWriter writer;

        public LogFileSimulator(IWriteSimulator write)
        {
            _write = write;
            writer = File.AppendText(_logFile);
        }

        public void LogDoorLocked(int Id)
        {
            using (writer)
            {
                _write.WriteLineLocked(writer, Id);
            }
        }

        public void LogDoorUnlocked(int Id)
        {
            using (writer)
            {
                _write.WriteLineUnlocked(writer, Id);
            }
        }
    }
}