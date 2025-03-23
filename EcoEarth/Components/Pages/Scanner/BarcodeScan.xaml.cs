using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ZXing.Net.Maui;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Mvc;

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

    //TODO: Fix nav manager null issue
    private void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        // Ensure the event is handled on the main thread
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var result = e.Results[0].Value.ToString();

            if (result == null)
                return;
            // Validating that the barcode is in the valid format
            if (validateBarcodeNumber(result))
            {

                // If so, take user to product info page (scanner/scanner/productinfo/{result})
            }
        });
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        // redirect back to scanner page
    }

    protected bool validateBarcodeNumber(string barcode)
    {
        // Validate barcode using reg exp
        var regex = new System.Text.RegularExpressions.Regex(@"^(?:\d{8}|\d{12}|\d{13}|\d{6})$");
        return regex.IsMatch(barcode);
    }

}
