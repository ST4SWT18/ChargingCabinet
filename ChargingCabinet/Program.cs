using System;
using ChargingCabinet.Interfaces;
using ChargingCabinet.Simulators;

namespace ChargingCabinet.Application
{
    class Program
    {
        static void Main(string[] args)
        {

            // Assemble your system here from all the classes
            IDoorSimulator door = new DoorSimulator();
            IUsbCharger usb = new UsbChargerSimulator();
            IDisplaySimulator display = new DisplaySimulator();
            ILogFileSimulator log = new LogFileSimulator();
            RfidReaderSimulator rfidReader = new RfidReaderSimulator();
            IChargeControl chargeControl = new ChargeControl(display, usb);
            IStationControl stationControl = new StationControl(door,chargeControl,display,log);

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
