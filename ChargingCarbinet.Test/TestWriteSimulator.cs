using System;
using System.IO;
using ChargingCabinet.Simulators;
using NSubstitute;
using NUnit.Framework;

namespace ChargingCarbinet.UnitTests
{
    public class TestWriteSimulator
    {
        private WriteSimulator _uut;
        private StringWriter _output;

        [SetUp]
        public void Setup()
        {
            _uut = new WriteSimulator();

            _output = new StringWriter();
            System.Console.SetOut(_output);
        }

        [TestCase(50)]
        public void LockedMessage_OutputString_IsEqualTo_Expected(int id)
        {
            string expected = DateTime.Now + ": Skab låst med RFID: " + id;

            string result = _uut.LockedMessage(id);

            Assert.AreEqual(expected, result);
        }

        [TestCase(50)]
        public void UnlockedMessage_OutputString_IsEqualTo_Expected(int id)
        {
            string expected = DateTime.Now + ": Skab låst op med RFID: " + id;

            string result = _uut.UnlockedMessage(id);

            Assert.AreEqual(expected, result);
        }
    }
}