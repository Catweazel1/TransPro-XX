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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TransPro_XX
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            LstBxItmProjectForm.IsSelected = true;
            BackButton.Visibility = Visibility.Collapsed;
            TxtBlckTitle.Text = "Project Formulier";
            MainFrame.Navigate(typeof(Project));
        }

        private void LstBxIcons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LstBxItmProjectForm.IsSelected)
            {
                BackButton.Visibility = Visibility.Collapsed;
                TxtBlckTitle.Text = "Project Formulier";
                MainFrame.Navigate(typeof(Project));
            }
            else if (LstBxItmTransportStickers.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                TxtBlckTitle.Text = "Transport Stickers";
                MainFrame.Navigate(typeof(Transport));
            }
            else if (LstBxItmSettings.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                TxtBlckTitle.Text = "Instellingen";
                MainFrame.Navigate(typeof(Settings));
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private async void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            var obj = App.Current as App;

            switch (TxtBlckTitle.Text)
            {
                case "Project Formulier":
                    ContentDialog dlg = new ContentDialog()
                    {
                        Title = "Project Formulier",
                        Content = $"Dit is een Project Formulier: {obj.ProjectNumber}",
                        PrimaryButtonText = "OK"
                    };
                    await dlg.ShowAsync();
                    break;
                case "Transport Stickers":
                    dlg = new ContentDialog()
                    {
                        Title = "Transport Stickers",
                        Content = "Dit zijn dan de Transport Stickers",
                        PrimaryButtonText = "OK"
                    };
                    await dlg.ShowAsync();
                    break;
                default:
                    break;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
                LstBxItmProjectForm.IsSelected = true;
            }
        }
    }
}
