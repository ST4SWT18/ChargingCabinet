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

        //Må man det her når ChargeControl ikke rigtigt kender RfidReaderSimulator?
        private IRfidReaderSimulator _rfidReaderSimulator;

        private ChargeControl _uut;
        private StringWriter _output;

        [SetUp]
        public void Setup()
        {
            _displaySimulator = Substitute.For<IDisplaySimulator>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _rfidReaderSimulator = Substitute.For<IRfidReaderSimulator>();
            _uut = new ChargeControl(_displaySimulator, _usbCharger);

            _output = new StringWriter();
            System.Console.SetOut(_output);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsConnected_CorrectValueIsReturned(bool expected)
        {
            _usbCharger.Connected = expected;

            Assert.That(_uut.IsConnected, Is.EqualTo(expected));
        }

        [Test]
        public void CheckIf_StartCharge_CallsStartCharge()
        {
            _uut.StartCharge();
            _usbCharger.Received(1).StartCharge();
        }

        [Test]
        public void CheckIf_StopCharge_CallsStopCharge()
        {
            _uut.StopCharge();
            _usbCharger.Received(1).StopCharge();
        }


        // Grænseværdi der ikke skal kalde noget
        [Test]
        public void CheckIf_NoDisplayMethods_IsCalled_WhenCurrentCurrentIs0()
        {
            _uut.CurrentCurrent = 0;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = _uut.CurrentCurrent });
            _displaySimulator.DidNotReceive().ShowCurrentlyChargingMessage();
            _displaySimulator.DidNotReceive().ShowPhoneChargingMessage();
            _displaySimulator.DidNotReceive().ShowCurrentErrorMessage();
            _displaySimulator.DidNotReceive().ShowFullyChargedMessage();
            _displaySimulator.DidNotReceive().ShowConnectPhoneMessage();
            _displaySimulator.DidNotReceive().ShowConnectionErrorMessage();
            _displaySimulator.DidNotReceive().ShowReadRfidMessage();
            _displaySimulator.DidNotReceive().ShowRfidErrorMessage();
            _displaySimulator.DidNotReceive().ShowTakePhoneAndCloseDoorMessage();
        }

        // Grænseværdier der skal kalde ShowFullyChargedMessage
        [TestCase(0.0001)]
        [TestCase(2.5)]
        [TestCase(5)]
        public void CheckIf_ShowFullyChargedMessage_IsCalled_WhenCurrentCurrentIsHigherThan0AndEqualToOrLessThan5(double currentCurrent)
        {
            _uut.CurrentCurrent = currentCurrent;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = _uut.CurrentCurrent });
            _displaySimulator.Received(1).ShowFullyChargedMessage();
        }

        // Grænseværdi der IKKE skal kalde ShowFullyChargedMessage
        [TestCase(0)]
        public void CheckIf_ShowFullyChargedMessage_IsNotCalled_WhenCurrentCurrentIs0(double currentCurrent)
        {
            _uut.CurrentCurrent = currentCurrent;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = _uut.CurrentCurrent });
            _displaySimulator.DidNotReceive().ShowFullyChargedMessage();
        }

        // Ugyldig værdi - kaster exception
        [TestCase(-1)]
        public void CheckIf_ExceptionIsThrown_WhenCurrentCurrentIsMinus1(double currentCurrent)
        {
            _uut.CurrentCurrent = currentCurrent;
            Assert.That(() => _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs()
            { Current = _uut.CurrentCurrent }), Throws.TypeOf<ArgumentException>());
        }

        // Grænseværdier der skal kalde ShowCurrentlyChargingMessage
        [TestCase(5.0001)]
        [TestCase(250)]
        [TestCase(500)]
        public void CheckIf_ShowCurrentlyChargingMessage_IsCalled_WhenCurrentCurrentIsHigherThan5AndEqualToOrLessThan500(double currentCurrent)
        {
            _uut.CurrentCurrent = currentCurrent;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = _uut.CurrentCurrent });
            _displaySimulator.Received(1).ShowCurrentlyChargingMessage();
        }

        // Grænseværdi der IKKE skal kalde ShowCurrentlyChargingMessage
        [TestCase(5)]
        public void CheckIf_ShowCurrentlyChargingMessage_IsNotCalled_WhenCurrentCurrentIs5(double currentCurrent)
        {
            _uut.CurrentCurrent = currentCurrent;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = _uut.CurrentCurrent });
            _displaySimulator.DidNotReceive().ShowCurrentlyChargingMessage();
        }

        // Grænseværdier der skal kalde ShowCurrentErrorMessage
        [TestCase(500.0001)]
        [TestCase(700)]
        public void CheckIf_ShowFullyChargedMessage_IsCalled_WhenCurrentCurrentIsHigherThan500(double currentCurrent)
        {
            _uut.CurrentCurrent = 600;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = _uut.CurrentCurrent });
            _displaySimulator.Received(1).ShowCurrentErrorMessage();
            _usbCharger.Received(1).SimulateOverload(true);
        }

        // Grænseværdi der IKKE skal kalde ShowCurrentErrorMessage
        [TestCase(500)]
        public void CheckIf_ShowFullyChargedMessage_IsNotCalled_WhenCurrentCurrentIs500(double currentCurrent)
        {
            _uut.CurrentCurrent = 500;
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = _uut.CurrentCurrent });
            _displaySimulator.DidNotReceive().ShowCurrentErrorMessage();
        }
    }
}
