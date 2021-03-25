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
            IWriteSimulator write = new WriteSimulator();
            ILogFileSimulator log = new LogFileSimulator(write);
            IRfidReaderSimulator rfidReader = new RfidReaderSimulator();
            IChargeControl chargeControl = new ChargeControl(display, usb);
            IStationControl stationControl = new StationControl(door,chargeControl,display,log,rfidReader);

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R, T: ");
                input = Console.ReadLine().ToString().ToLower();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'e':
                        finish = true;
                        break;

                    case 'o':
                        door.OnDoorOpen(true);
                        Console.WriteLine("Tryk T");
                        break;
                    case 't':
                        usb.SimulateConnected(true);
                        break;

                    case 'c':
                        door.OnDoorClose(true);
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
