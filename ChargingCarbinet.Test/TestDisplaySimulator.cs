using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;
using NUnit.Framework;

namespace ChargingCarbinet.UnitTests
{
    public class TestDisplaySimulator
    {
        private IDisplaySimulator _uut;
        private StringWriter _output;

        [SetUp]
        public void Setup()
        {
            _uut = new DisplaySimulator();
            _output = new StringWriter();
            System.Console.SetOut(_output);
        }

        [Test]
        public void ShowConnecPhoneMessage_Test()
        {
            string expected = "Tilslut telefon\r\n";

            _uut.ShowConnectPhoneMessage();

            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShowReadRfidMessage_Test()
        {
            string expected = "Indlæs RFID\r\n";

            _uut.ShowReadRfidMessage();

            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShowPhoneChargingMessage_Test()
        {
            string expected = "Ladeskab optaget\r\n";

            _uut.ShowPhoneChargingMessage();

            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShowConnectionErrorMessage_Test()
        {
            string expected = "Tilslutningsfejl\r\n";

            _uut.ShowConnectionErrorMessage();

            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShowRfidErrorMessage_Test()
        {
            string expected = "RFID fejl\r\n";

            _uut.ShowRfidErrorMessage();

            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShowTakePhoneAndCloseDoorMessage_Test()
        {
            string expected = "Fjern telefon\r\n";

            _uut.ShowTakePhoneAndCloseDoorMessage();

            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShowFullyChargedMessage_Test()
        {
            string expected = "Din telefon er fuldt opladet\r\n";

            _uut.ShowFullyChargedMessage();

            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShowCurrentErrorMessage_Test()
        {
            string expected = "Der skete en fejl. Opladning er stoppet.\r\n";

            _uut.ShowCurrentErrorMessage();

            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShowCurrentlyChargingMessage_Test()
        {
            string expected = "Telefonen oplades\r\n";

            _uut.ShowCurrentlyChargingMessage();

            string result = _output.ToString();

            Assert.AreEqual(expected, result);
        }
    }
}
