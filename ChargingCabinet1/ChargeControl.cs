using System;
using ChargingCabinet.Events;
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
        public double CurrentCurrent { get; set; }

        public ChargeControl(IDisplaySimulator displaySimulator, IUsbCharger usbCharger)
        {
            _displaySimulator = displaySimulator;
            _usbCharger = usbCharger;

            _usbCharger.CurrentValueEvent += NewCurrentValue;
        }

        private void NewCurrentValue(object sender, CurrentEventArgs e)
        {
            CurrentCurrent = e.Current;

            if (CurrentCurrent == 0)
            {

            }
            else if (CurrentCurrent > 0 && CurrentCurrent <= 5)
            {
                _displaySimulator.ShowFullyChargedMessage();
            }
            else if (CurrentCurrent > 5 && CurrentCurrent <= 500)
            {
                _displaySimulator.ShowCurrentlyChargingMessage();
            }
            else if (CurrentCurrent > 500)
            {
                _displaySimulator.ShowCurrentErrorMessage();
                _usbCharger.SimulateOverload(true);
                StopCharge();
            }
            else
            {
                throw new ArgumentException("Index is out of range");
            }
        }

        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }
    }
}