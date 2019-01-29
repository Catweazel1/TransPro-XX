using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Printing;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static TransPro_XX.MainPage;
using TransPro_XX.classes;
using static TransPro_XX.classes.PrinterEnumeration;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TransPro_XX
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        private DeviceInformationCollection deviceCollection;

        public Settings()
        {
            this.InitializeComponent();
        }

        private async void CmbBxFormPrinter_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage rootPage = MainPage.Current;

            try
            {
                rootPage.NotifyUser("Enumerating printers. Please wait", NotifyType.StatusMessage);

                // Retrieve the running app&#39;s package family name, and enumerate associated printers
                string currentPackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;

                // Enumerate associated printers.
                PrinterEnumeration pe = new PrinterEnumeration(currentPackageFamilyName);
                List<PrinterInfo> associatedPrinters = await pe.EnumeratePrinterAsync();

                // Update the datae binding source on the combo box that display the list of printers
                CmbBxFormPrinter.ItemsSource = associatedPrinters;
                if (associatedPrinters.Count > 0)
                {
                    CmbBxFormPrinter.SelectedIndex = 0;
                    rootPage.NotifyUser(associatedPrinters.Count + " printers enumerated", NotifyType.StatusMessage);
                }
                else
                {
                    rootPage.NotifyUser(DisplayStrings.NoPrintersEnumerated, NotifyType.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                rootPage.NotifyUser("Caught an exception: " + ex.Message, NotifyType.ErrorMessage);
            }
        }
    }
}