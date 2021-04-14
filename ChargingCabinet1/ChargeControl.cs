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
        //public double CurrentCurrent { get; set; }

        public ChargeControl(IDisplaySimulator displaySimulator, IUsbCharger usbCharger)
        {
            _displaySimulator = displaySimulator;
            _usbCharger = usbCharger;

            _usbCharger.CurrentValueEvent += NewCurrentValue;
        }

        private void NewCurrentValue(object sender, CurrentEventArgs e)
        {
            //CurrentCurrent = e.Current;

            if (e.Current == 0)
            {
                _usbCharger.SimulateConnected(false);
            }
            else if (e.Current > 0 && e.Current <= 5)
            {
                _displaySimulator.ShowFullyChargedMessage();
                StopCharge();
            }
            else if (e.Current > 5 && e.Current <= 500)
            {
                _displaySimulator.ShowCurrentlyChargingMessage();
            }
            else if (e.Current > 500)
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