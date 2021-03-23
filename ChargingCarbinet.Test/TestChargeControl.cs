using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChargingCabinet;
using ChargingCabinet.Events;
using ChargingCabinet.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ChargingCarbinet.UnitTests
{
    public class TestChargeControl
    {
        private IDisplaySimulator _displaySimulator;
        private IUsbCharger _usbCharger;
        private ChargeControl _uut;
        private StringWriter _output;

        [SetUp]
        public void Setup()
        {
            _displaySimulator = Substitute.For<IDisplaySimulator>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_displaySimulator, _usbCharger);

            _output = new StringWriter();
            System.Console.SetOut(_output);
        }

        [TestCase(true)]
        public void IsConnected_IsEqualTo_ArgumentTrue(bool newBool)
        {
            _usbCharger.Connected = true;

            Assert.That(_uut.IsConnected, Is.EqualTo(newBool));
        }

        [TestCase(false)]
        public void IsConnected_IsEqualTo_ArgumentFalse(bool newBool)
        {
            _usbCharger.Connected = false;

            Assert.That(_uut.IsConnected, Is.EqualTo(newBool));
        }
    }
}
