using System;

namespace ChargingCabinet.Interfaces
{
    public interface IChargeControl
    {
        bool IsConnected();

        void StartCharge();

        void StopCharge();
        
    }
}