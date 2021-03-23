using System;
using System.Collections.Generic;
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

        [SetUp]
        public void Setup()
        {
            _displaySimulator = Substitute.For<IDisplaySimulator>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_displaySimulator, _usbCharger);
        }


        [Test]
        public void test()
        {
        }
    }
}
