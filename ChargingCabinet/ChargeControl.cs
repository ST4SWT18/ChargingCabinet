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

        public ChargeControl(IDisplaySimulator displaySimulator, IUsbCharger usbCharger)
        {
            _displaySimulator = displaySimulator;
            _usbCharger = usbCharger;

            _usbCharger.CurrentValueEvent += NewCurrentValue;
        }

        private void NewCurrentValue(object sender, CurrentEventArgs e)
        {
            if (_usbCharger.CurrentValue == 0)
            {
                
            }
            else if (_usbCharger.CurrentValue > 0 && _usbCharger.CurrentValue <= 5)
            {
                _displaySimulator.ShowFullyChargedMessage();
            }
            else if (_usbCharger.CurrentValue > 5 && _usbCharger.CurrentValue <= 500)
            {
                _displaySimulator.ShowCurrentlyChargingMessage();
            }
            else if (_usbCharger.CurrentValue > 500)
            {
                _displaySimulator.ShowCurrentErrorMessage();
                StopCharge();
            }
        }


        public bool IsConnected()
        {
            _usbCharger.s
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