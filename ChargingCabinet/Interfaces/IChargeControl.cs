using System;

namespace ChargingCabinet.Interfaces
{
    public interface IChargeControl
    {
        bool IsConnected();
        void StartCharge(object sender, EventArgs e);

        void StopCharge(object sender, EventArgs e);
        
    }
}