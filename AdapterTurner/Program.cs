using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using DisableDevice;
using Microsoft.Win32;

namespace AdapterTurner
{
    class Program
    {
        static void Main(string[] args)
        {
            string FirstGuid = "", FirstCommand = "";
            Guid classGuid = new Guid(@"{4D36E972-E325-11CE-BFC1-08002BE10318}");
            AdaptersData Adapters = new AdaptersData();
            Console.WriteLine("Список доступных сетевых устройств: ");
            Adapters.writeAdaptersList();
            if (args.Length > 1 && args.Length < 3)
            {
                FirstGuid = args[0];
                FirstCommand = args[1];
                string instancePath="";
                try
                {
                    instancePath = Adapters.FindInstanceIdByGUID(FirstGuid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                if (FirstCommand == "-e")
                {
                    try
                    {
                        DeviceHelper.SetDeviceEnabled(classGuid, instancePath, true);
                        Console.WriteLine("success: Устройство подключено успешно");
                    }
                    catch (System.ComponentModel.Win32Exception ex)
                    {
                        Console.WriteLine("err: " + ex.Message);
                    }
                }
                else if (FirstCommand == "-d")
                    try
                    {
                        DeviceHelper.SetDeviceEnabled(classGuid, instancePath, false);
                        Console.WriteLine("success: Устройство отключено успешно");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("err: " + ex.Message);
                    }
                else Console.WriteLine("err: Неправильная команда");

            }
            else
            {
                Console.WriteLine("err: Не верное количество аргументов");
                Console.WriteLine();
                Console.WriteLine("Введите вашу команду eще раз");
                string Command = Console.ReadLine();
                string GUID = "", CommandKey = "";
                for (int i = 0; i < Command.Length; i++)
                {
                    if (Command[i] == ' ')
                    {
                        GUID = Command.Substring(0, i);
                        CommandKey = Command.Substring(i + 1, 2);
                    }
                }
                string instancePath = "";
                try
                {
                    instancePath = Adapters.FindInstanceIdByGUID(GUID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);                    
                    return;
                }
                if (CommandKey == "-e")
                {
                    try
                    {
                        DeviceHelper.SetDeviceEnabled(classGuid, instancePath, true);
                        Console.WriteLine("success: Устройство подключено успешно");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("err: " + ex.Message);
                    }
                }
                else if (CommandKey == "-d")
                    try
                    {
                        DeviceHelper.SetDeviceEnabled(classGuid, instancePath, false);
                        Console.WriteLine("success: Устройство отключено успешно");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("err: " + ex.Message);
                    }
                else Console.WriteLine("err: Неправильная команда");
                Console.ReadLine();
            }
        }
    }
}
