// File: Components/Pages/Scanner/BarcodeScan.xaml.cs
using ZXing.Net.Maui;

namespace EcoEarthPOC.Components.Pages.Scanner
{
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

        public void OnToggleTorchClicked(object sender, EventArgs e)
        {
            barcodeReader.IsTorchOn = !barcodeReader.IsTorchOn;
        }

        public void OnSwitchCameraClicked(object sender, EventArgs e)
        {
            barcodeReader.CameraLocation = barcodeReader.CameraLocation == CameraLocation.Front ? CameraLocation.Rear : CameraLocation.Front;
        }

        public async void OnBackToScannerMenuClicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }




        protected bool validateBarcodeNumber(string barcode)
        {
            // Validate barcode using reg exp
            var regex = new System.Text.RegularExpressions.Regex(@"^(?:\d{8}|\d{12}|\d{13}|\d{6})$");
            return regex.IsMatch(barcode);
        }
    }
}
