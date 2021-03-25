using System.Collections.Generic;
using System.IO;
using System.Text;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace ChargingCarbinet.UnitTests
{
    public class TestLogFileSimulator
    {
        //UDKAST TIL TEST AF LOGFILESIMULATOR - FORKLARING FINDES I RAPPORT

        private LogFileSimulator _uut;
        private IWriteSimulator _write;
        private StringWriter _output;

        [SetUp]
        public void Setup()
        {
            _write = Substitute.For<IWriteSimulator>();
            _uut = new LogFileSimulator(_write);

            _output = new StringWriter();
            System.Console.SetOut(_output);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(1000)]
        public void LogDoorLocked_CallsWriteLineLocked(int id)
        {
            _uut.LogDoorLocked(id);
            _write.Received(1).WriteLineLocked(id);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(1000)]
        public void LogDoorUnlocked_CallsWriteLineUnlocked(int id)
        {
            _uut.LogDoorUnlocked(id);
            _write.Received(1).WriteLineUnlocked(id);
        }
    }
}
