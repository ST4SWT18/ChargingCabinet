using System;
using ChargingCabinet.Events;

namespace ChargingCabinet.Interfaces
{
    public interface IDisplaySimulator
    {
        void ShowConnectPhoneMessage();
        void ShowReadRfidMessage();
        void ShowPhoneChargingMessage();
        void ShowConnectionErrorMessage();
        void ShowRfidErrorMessage();
        void ShowTakePhoneAndCloseDoorMessage();
        void ShowFullyChargedMessage();
        void ShowCurrentErrorMessage();
        void ShowCurrentlyChargingMessage();
    }
}