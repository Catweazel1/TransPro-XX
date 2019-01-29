﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Enumeration;
using Windows.Devices.Enumeration.Pnp;

namespace TransPro_XX.classes
{
    /// <summary>
    /// Contains methods to perform enumeration of printers associated with a app package family name.
    /// </summary>
    class PrinterEnumeration
    {
        internal PrinterEnumeration(string packageFamilyName)
        {
            this.packageFamilyNameToMatch = packageFamilyName;
            printerList = new List<PrinterInfo>();
        }

        /// <summary>
        /// Asynchronously enumerate the list of printer associated with the app's
        /// package family name provided in the constructor.
        /// </summary>
        internal async Task<List<PrinterInfo>> EnumeratePrinterAsync()
        {
            // This selection filter will filter for printer device interfaces.
            string printerInterfaceClass = "{0ecef634-6ef0-472a-8085-5ad023ecbccd}";
            string selectorFilter = "System.Devices.InterfaceClassGuid:=\"" + printerInterfaceClass + "\"";

            // Look for the Container ID in each device interface
            string containerIdPropertyName = "System.Devices.ContainerId";
            string[] propertiesToRetrieve = new string[] { containerIdPropertyName };

            //Asynchronously find all printer device interfaces.
            DeviceInformationCollection printerInterfaceCollection = await DeviceInformation.FindAllAsync(selectorFilter, propertiesToRetrieve);

            //For each printer device returned, retrieve the device container, which contains
            //The association information for the Windows Store Device Apps.
            foreach (DeviceInformation printerInterfaceInformation in printerInterfaceCollection)
            {
                string deviceContainerId = printerInterfaceInformation.Properties[containerIdPropertyName].ToString();

                if(await IsDeviceContainerAssociatedWithAppAsync(deviceContainerId))
                {
                    printerList.Add(new PrinterInfo(
                        (string)printerInterfaceInformation.Properties["System.ItemNameDisplay"],
                        (string)printerInterfaceInformation.Id);
                }
            }
            return printerList;
        }

        /// <summary>
        /// Determins if a printer device container is associated with the Windows Store Device App
        /// </summary>
        async Task<bool> IsDeviceContainerAssociatedWithAppAsync(string deviceContainerId)
        {
            //CreateFromIdAsync needs braces on the containerId string.
            string formattedContainerId = "{" + deviceContainerId + "}";

            // Retrieve the 'package family name' property from the device container which tells
            string packageFamilyNamePropertyName = "System.Devices.AppPackageFamilyName";
            string[] propertiesToRetrieve = new string[] { packageFamilyNamePropertyName };

            //Asyncronously retrieve the device container information.
            PnpObject deviceContainer = await PnpObject.CreateFromIdAsync(PnpObjectType.DeviceContainer, formattedContainerId, propertiesToRetrieve);

            // If the packageFamilyName of the printer container matches the one for this app, the printer is associated with this app.
            string[] packageFamilyNameList = (string[])deviceContainer.Properties[packageFamilyNamePropertyName];

            if(packageFamilyNameList != null)
            {
                foreach (string packageFamilyName in packageFamilyNameList)
                {
                    if(packageFamilyName.CompareTo(this.packageFamilyNameToMatch)== 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        ///<summary>
        ///Contains the list of associated printers.
        ///</summary>
        private List<PrinterInfo> printerList;

        /// <summary>
        /// Package family name for which associated printers are to be enumerated.
        /// </summary>
        private string packageFamilyNameToMatch;

        internal class PrinterInfo
        {
            public PrinterInfo(string printerName, string deviceId)
            {
                Name = printerName;
                DeviceId = deviceId;
            }

            /// <summary>
            /// Name of the print queue
            /// </summary>
            public string Name
            {
                get;
                private set;
            }

            /// <summary>
            /// Device id of the print queue
            /// </summary>
            public string DeviceId
            {
                get;
                private set;
            }
        }
    }
}
