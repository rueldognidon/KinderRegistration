
using System;
using KinderRegistartion.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace KinderRegistartion
{
    public partial class App : Application
    {
        public static NavigationPage Navigation { get; set; } = new NavigationPage();
        public static AzureService Service { get; set; } = new AzureService();
        public App()
        {
            InitializeComponent();

            Navigation.BarBackgroundColor = Color.FromHex("011d33");
            Navigation.BarTextColor = Color.White;
            
             

            MainPage = Navigation;
            Navigation.PushAsync(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
