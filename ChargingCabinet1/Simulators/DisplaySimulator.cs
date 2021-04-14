using System;
using System.Threading.Channels;
using ChargingCabinet.Interfaces;

namespace ChargingCabinet.Simulators
{
    public class DisplaySimulator : IDisplaySimulator
    {
        /*Forslag til generel metode, hvor metoden er parametriseret med en string, som man ønskede at display.
         Dette kunne have reduceret koden for klassen til kun én konstruktor og en display metode med en message string parameter.
        Det gør det også nemmere, hvis man nu havde rigtig mange Message-metoder, som vi har nu, så man ikke skal finde rundt i dem.*/

        //public void DisplayMessage(string message)
        //{
        //    Console.WriteLine(message);
        //}

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