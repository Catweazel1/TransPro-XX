using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransPro_XX.classes
{
    internal class DisplayStrings
    {
        internal const string NoPrintersEnumerated =
            "No printers were enumerated. \r\n" +
            "Please verify you have the appropriate device metadata staged in the system's" +
            "local metadata store.\r\n" +
            "Device metadata may be authored and staged using the device metadata authoring wizard\r\n" +
            "http://msdn.microsoft.com/en-us/library/windows/hardware/hh454213(v=vs.85).aspx";

        internal const string PrintersEnumerating = "Enumerating printers. Please wait";

        internal const string PrinterEnumerated = "Printers enumerated. Please select a printer to proceed";

        internal const string EnumeratePrintersToContinue = "Please enumerate printers to continue";

        // Private constructor to prevent default construction. 
        private DisplayStrings()
        { }
    }
}
