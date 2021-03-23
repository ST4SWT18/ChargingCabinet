using System;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;

namespace ChargingCabinet
{
    /// <summary>
    /// Controller-klassen for opladningen
    /// </summary>
    public class ChargeControl : IChargeControl
    {
        private IDisplaySimulator _displaySimulator;
        private IUsbCharger _usbCharger;

        public ChargeControl(IDisplaySimulator displaySimulator, IUsbCharger usbCharger)
        {
            _displaySimulator = displaySimulator;
            _usbCharger = usbCharger;

            _usbCharger.CurrentValueEvent += StartCharge;
        }


        public bool IsConnected()
        {
            return true;//skal ændres
        }

        public void StartCharge(object sender, EventArgs e)
        {
            _usbCharger.StartCharge();
            
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
            Console.WriteLine("Stopped charging");
        }
    }
}