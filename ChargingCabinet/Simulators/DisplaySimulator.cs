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
            Console.WriteLine("Ladeskab optaget");
        }

        public void ShowConnectionErrorMessage()
        {
            Console.WriteLine("Tilslutningsfejl");
        }

        public void ShowRfidErrorMessage()
        {
            Console.WriteLine("RFID fejl");
        }

        public void ShowTakePhoneAndCloseDoorMessage()
        {
            Console.WriteLine("Fjern telefon");
        }

        public void ShowFullyChargedMessage()
        {
            Console.WriteLine("Din telefon er fuldt opladet");
        }

        public void ShowCurrentErrorMessage()
        {
            Console.WriteLine("Der skete en fejl. Opladning er stoppet.");
        }

        public void ShowCurrentlyChargingMessage()
        {
            Console.WriteLine("Telefonen oplades");
        }
    }
}