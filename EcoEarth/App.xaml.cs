namespace EcoEarthPOC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("barcodescantest", typeof(Components.Pages.Scanner.BarcodeScanTest));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "EcoEarthPOC" };
        }

    }
}
