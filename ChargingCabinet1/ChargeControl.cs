﻿using System;
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
        public double CurrentCurrent { get; set; } //hehe

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
            if (CurrentCurrent > 0 && CurrentCurrent <= 5)
            {
                _displaySimulator.ShowFullyChargedMessage();
            }
            if (CurrentCurrent > 5 && CurrentCurrent <= 500)
            {
                _displaySimulator.ShowCurrentlyChargingMessage();
            }
            if (CurrentCurrent > 500)
            {
                _displaySimulator.ShowCurrentErrorMessage();
                StopCharge();
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