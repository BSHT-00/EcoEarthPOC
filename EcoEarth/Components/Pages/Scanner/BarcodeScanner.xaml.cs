namespace EcoEarthPOC.Components.Pages.Scanner;

using ZXing;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

public partial class BarcodeScanner : ContentView
{
    public BarcodeScanner()
    {
        InitializeComponent();

        // Initialize barcode reader

        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            AutoRotate = true,
            Multiple = false,
            TryInverted = true,
            TryHarder = true,
        };

        // Subscribe to the BarcodesDetected event
        barcodeReader.BarcodesDetected += barcodeReader_BarcodesDetected;
    }

    private void barcodeReader_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var result = e.Results.FirstOrDefault();

        // Check if a barcode was detected
        if (result == null)
            return;

        // Handle the detected barcode
        // For example, display the barcode value
        Console.WriteLine($"Detected barcode: {result.Value}");
    }
}
