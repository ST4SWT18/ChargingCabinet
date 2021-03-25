using System;
using System.IO;
using ChargingCabinet.Simulators;
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
        public void test(int id)
        {
            string expected = DateTime.Now + ": Skab låst med RFID: " + id + "\r\n";

            _uut.WriteLineLocked(id);
            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }

        [TestCase(50)]
        public void test2(int id)
        {
            string expected = DateTime.Now + ": Skab låst op med RFID: " + id + "\r\n";

            _uut.WriteLineUnlocked(id);
            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }
    }
}