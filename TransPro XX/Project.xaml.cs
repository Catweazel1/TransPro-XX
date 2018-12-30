using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class Project : Page
    {
        public Project()
        {
            this.InitializeComponent();
            CalDateComplete.IsEnabled = false;

        }

        private void ChkBxComplete_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CalDateComplete.IsEnabled = !CalDateComplete.IsEnabled;
            CalDateComplete.Date = DateTime.Today;
        }

        private void TxtEndUser_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
