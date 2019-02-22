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
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Printing;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TransPro_XX
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProjectPageToPrint : Page
    {
        protected PrintDocument printDocument;
        protected IPrintDocumentSource printDocumentSource;
        internal List<UIElement> printPreveiewPages;
        protected FrameworkElement firstPage;
        protected Canvas PrintCanvas => FindName("ProjectPrintPage") as Canvas;

        public ProjectPageToPrint()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RegisterForPrinting();
            PreparePrintContent(new ProjectPageToPrint());
            ContentDialog dlg = new ContentDialog()
            {
                Title = "Print Status",
                Content = "Print contract registered with customization, use the Print button to print",
                PrimaryButtonText = "OK"
            };
        }
       
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            UnregisterForPrinting();
        }

        public virtual void RegisterForPrinting()
        {
            printDocument = new PrintDocument();
            printDocumentSource = printDocument.DocumentSource;
            printDocument.Paginate += CreatePrintPreviewPages;
            printDocument.GetPreviewPage += GetPreviewPage;
            printDocument.AddPages += AddPrintPages;

            PrintManager printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested += PrintTaskRequested;
        }

        private void PreparePrintContent(Page page)
        {
            if(firstPage == null)
            {
                firstPage = page;
                StackPanel header = (StackPanel)firstPage.FindName("Header");
                header.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            // Add the (newly created) page to the print canvas which is part of the visual tree and force it to go
            // through layout so that the linked containers correctly distribute the content inside them.
            PrintCanvas.Children.Add(firstPage);
            PrintCanvas.InvalidateMeasure();
            PrintCanvas.UpdateLayout();
        }

        public virtual void UnregisterForPrinting()
        {
            if(printDocument == null)
            {
                return;
            }

            printDocument.Paginate -= CreatePrintPreviewPages;
            printDocument.GetPreviewPage -= GetPrintPreviewPage;
            printDocument.AddPages -= AddPrintPages;

            // Remove the handler for printing initialization.
            PrintManager printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested -= PrintTaskRequested;

            PrintCanvas.Children.Clear();
        }
    }
}
