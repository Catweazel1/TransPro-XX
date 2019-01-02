﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            deviceCollection = await DeviceInformation.FindAllAsync("System.Devices.InterfaceClassGuid:=\"{0ecef634-6ef0-472a-8085-5ad023ecbccd}\"");
        }
    }
}
