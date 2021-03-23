using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChargingCabinet.Simulators;
using NUnit.Framework;

namespace ChargingCarbinet.UnitTests
{
    public class TestDisplaySimulator
    {
        private DisplaySimulator _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new DisplaySimulator();

        }

        [Test]
        public void ShowConnecPhoneMessage_Test()
        {

            //string expected = "Tilslut telefon";
            //_uut.ShowConnectPhoneMessage();

            //string text = Console.ReadLine();

            //var output = new StringWriter();
            //Console.SetOut(output);
            //var text = output.ToString();

            
            //Assert.AreEqual(expected, text);
        }
    }
}
