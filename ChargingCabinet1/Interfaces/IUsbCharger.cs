using System;
using ChargingCabinet.Events;

namespace ChargingCabinet.Interfaces
{
    public interface IUsbCharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;

        // Direct access to the current current value
        double CurrentValue { get; set; }

        // Require connection status of the phone
        bool Connected { get; set; }

        void SimulateConnected(bool connected);

        // Start charging
        void StartCharge();

        // Stop charging
        void StopCharge();
    }
}