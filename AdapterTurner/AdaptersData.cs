using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterTurner
{
    class AdaptersData
    {
        Dictionary<String, String> Adapters;

        public AdaptersData() {
        Adapters = new Dictionary<String, String>();
            //   var rk = Registry.LocalMachine.OpenSubKey(@"\SYSTEM\ControlSet001\Control\Class\");
            //   var value = (byte[])rk.GetValue("Class");
            string vmsBaseKeyString = @"SYSTEM\ControlSet001\Control\Class\{4D36E972-E325-11CE-BFC1-08002BE10318}";
            RegistryKey vmsBaseKey = Registry.LocalMachine.OpenSubKey(vmsBaseKeyString);
            string[] subNames = vmsBaseKey.GetSubKeyNames();
            foreach (var subName in subNames)
            {
                if (subName != "Properties")
                {
                    RegistryKey potentialVmKey = vmsBaseKey.OpenSubKey(subName, true);
                    Adapters.Add((String)potentialVmKey.GetValue("NetCfgInstanceId"), (String)potentialVmKey.GetValue("DeviceInstanceID"));
                }
            }
        }

        public string FindInstanceIdByGUID(string GUID)
        {
            string result;
            Adapters.TryGetValue(GUID, out result);
            return result;
        }

        public void writeAdaptersList()
        {
            int i = 1;
            foreach (var adapter in Adapters)
            {
                Console.WriteLine(i.ToString()+ ") " + adapter.Key);
                ++i;
            }
        }

        public void RenewAdaptersList()
        {
            Adapters.Clear();
            //   var rk = Registry.LocalMachine.OpenSubKey(@"\SYSTEM\ControlSet001\Control\Class\");
            //   var value = (byte[])rk.GetValue("Class");
            string vmsBaseKeyString = @"SYSTEM\ControlSet001\Control\Class\{4D36E972-E325-11CE-BFC1-08002BE10318}";
            RegistryKey vmsBaseKey = Registry.LocalMachine.OpenSubKey(vmsBaseKeyString);
            string[] subNames = vmsBaseKey.GetSubKeyNames();
            foreach (var subName in subNames)
            {
                if (subName != "Properties")
                {
                    RegistryKey potentialVmKey = vmsBaseKey.OpenSubKey(subName, true);
                    Adapters.Add((String)potentialVmKey.GetValue("NetCfgInstanceId"), (String)potentialVmKey.GetValue("DeviceInstanceID"));
                }
            }
        }
    }
}
