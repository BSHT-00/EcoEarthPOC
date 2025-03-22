using ZXing.Net.Maui;

namespace EcoEarthPOC.Components.Pages.Scanner;

public partial class BarcodeScanTest : ContentPage
{
    public BarcodeScanTest()
    {
        InitializeComponent();

        barcodeReader.Options = new BarcodeReaderOptions()
        {
            AutoRotate = true,
            Formats = BarcodeFormat.Ean13 | BarcodeFormat.UpcA | BarcodeFormat.Ean8 | BarcodeFormat.UpcE,
            Multiple = false
        };
    }

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        // Ensure the event is handled on the main thread
        MainThread.BeginInvokeOnMainThread(() =>
        {
            // Update the label with the detected barcode value
            if (e.Results.Any())
            {
                barcodeResult.Text = e.Results.First().Value;
                Console.WriteLine(barcodeResult.Text);
            }
        });
    }
}
