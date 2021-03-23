using System;
using System.Threading.Channels;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet.Simulators
{
    public class DisplaySimulator : IDisplaySimulator
    {
        public void ShowConnectPhoneMessage()
        {
            Console.WriteLine("Tilslut telefon");
        }

        public void ShowReadRfidMessage()
        {
            Console.WriteLine("Indlæs RFID");
        }

        public void ShowPhoneChargingMessage()
        {
            Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void ShowConnectionErrorMessage()
        {
            Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        public void ShowRfidErrorMessage()
        {
            Console.WriteLine("Forkert RFID tag");
        }

        public void ShowTakePhoneAndCloseDoorMessage()
        {
            Console.WriteLine("Tag din telefon ud af skabet og luk døren");
        }
    }
}