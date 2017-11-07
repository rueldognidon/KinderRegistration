
using KinderRegistartion.Services;
using Xamarin.Forms;

namespace KinderRegistartion
{
    public partial class RegisteredPage : ContentPage
    {
        public RegisteredPage()
        {
            InitializeComponent();

            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var att = e.SelectedItem as AttendeeViewModel;

            MessagingCenter.Send<object, KinderRegistration>(this, "RegisteredLoaded", att.Entity);

            Navigation.PopAsync();
        }
    }
}