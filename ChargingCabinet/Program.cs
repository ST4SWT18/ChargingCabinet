using System;
using ChargingCabinet.Simulators;
using Ladeskab;
using UsbSimulator.Simulators;

namespace ChargingCabinet
{
    class Program
    {
        static void Main(string[] args)
        {

            // Assemble your system here from all the classes
            IDoorSimulator door = new DoorSimulator();
            IUsbCharger usb = new UsbChargerSimulator();
            IStationControl stationControl = new StationControl(usb,door);
            RfidReaderSimulator rfidReader = new RfidReaderSimulator(stationControl);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine().ToString().ToLower();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'e':
                        finish = true;
                        break;

                    case 'o':
                        door.OnDoorOpen();
                        break;

                    case 'c':
                        door.OnDoorClose();
                        break;

                    case 'r':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
