using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ZXing.Net.Maui;

namespace EcoEarthPOC.Components.Pages.Scanner;

public partial class BarcodeScanTest : ContentPage
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }

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
            if (e.Results == null)
                return;
            if (validateBarcodeNumber(e.Results.ToString()))
            {
                NavigationManager.NavigateTo("/scanner/productinfo/" + e.Results.ToString());
            }
        });
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (NavigationManager != null)
            NavigationManager.NavigateTo("scanner/scanner");
    }

    protected bool validateBarcodeNumber(string barcode)
    {
        // Validate barcode using reg exp
        var regex = new System.Text.RegularExpressions.Regex(@"^(?:\d{8}|\d{12}|\d{13}|\d{6})$");
        return regex.IsMatch(barcode);
    }
}
