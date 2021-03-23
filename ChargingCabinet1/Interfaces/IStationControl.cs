using System;

namespace ChargingCabinet.Interfaces
{
    public interface IStationControl
    {
        public void DoorOpened(object sender, EventArgs e);
        public void DoorClosed(object sender, EventArgs e);
        public void CheckId(int OldId, int Id);
    }
}