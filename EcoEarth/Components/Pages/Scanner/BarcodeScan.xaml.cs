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
using EcoEarthPOC.Components.Pages;

namespace EcoEarthPOC.Components.Pages.Scanner;

// https://pavlodatsiuk.hashnode.dev/implementing-maui-blazor-with-zxing-qr-barcode-scanner
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

        if (barcodeReader == null)
            return;
    }

    private TaskCompletionSource<string> scanTask = new TaskCompletionSource<string>();

    public void CameraBarcodeReaderView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        barcodeReader.IsDetecting = false;

        if (!validateBarcodeNumber(e.Results.FirstOrDefault().Value))
        {
            barcodeReader.IsDetecting = true;
            return;
        }

        Application.Current.MainPage.Navigation.PopModalAsync();
        var barcode = e.Results.FirstOrDefault()?.Value;
        scanTask.TrySetResult(barcode);
    }

    public async Task<string> WaitForResultAsync()
    {
        return await scanTask.Task;
    }


    protected bool validateBarcodeNumber(string barcode)
    {
        // Validate barcode using reg exp
        var regex = new System.Text.RegularExpressions.Regex(@"^(?:\d{8}|\d{12}|\d{13}|\d{6})$");
        return regex.IsMatch(barcode);
    }
}
