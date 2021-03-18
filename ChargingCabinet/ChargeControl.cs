using System;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;

namespace ChargingCabinet
{
    /// <summary>
    /// Controller-klassen for opladningen
    /// </summary>
    public class ChargeControl
    {
        private IDisplaySimulator _displaySimulator;
        private IUsbCharger _usbCharger;

        public ChargeControl(IDisplaySimulator displaySimulator, IUsbCharger usbCharger)
        {
            _displaySimulator = displaySimulator;
            _usbCharger = usbCharger;
        }


        public bool IsConnected()
        {
            return true;//skal ændres
        }

        public void StartCharge()
        {
            Console.WriteLine("Is charging");
        }

        public void StopCharge()
        {
            Console.WriteLine("Stopped charging");
        }
    }
}