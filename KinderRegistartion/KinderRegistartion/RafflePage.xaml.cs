
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KinderRegistartion
{
    public partial class RafflePage : ContentPage
    {
        Random random = new Random();
        RaffleViewModel viewModel;
        public RafflePage()
        {
            InitializeComponent();
            viewModel = new RaffleViewModel();
            this.BindingContext = viewModel;


        }

        private void ShowRaffle(object sender, System.EventArgs e)
        {
            RaffleView.IsVisible = true;
        }

        private async void PickRandom(object sender, EventArgs e)
        {

            var att = viewModel.AttendeeList;

            if(att == null || att.Count == 0)
            {
                await DisplayAlert(null, "No Attendees Yet!", "Close");
                return;
            }
            //var att = new List<AttendeeViewModel>
            //{
            //    new AttendeeViewModel{FullName = "Ruel"},
            //    new AttendeeViewModel{FullName = "PD"},
            //    new AttendeeViewModel{FullName = "Raymond"},
            //    new AttendeeViewModel{FullName = "Daryl"},
            //    new AttendeeViewModel{FullName = "Benjo"},
            //    new AttendeeViewModel{FullName = "Sarah"},
            //    new AttendeeViewModel{FullName = "Onel"},

            //};

            WinnerName.BackgroundColor = Color.Yellow;
            for (int i = 0; i < 20; i++)
            {
                WinnerName.Opacity = 0;
                
                var choice = random.Next(att.Count);
                var sel = att[choice];
                WinnerName.Text = sel.FullName;

                await WinnerName.FadeTo(1, (uint)(100 * i), Easing.CubicInOut);
            }

            WinnerName.BackgroundColor = Color.LightGreen;
        }

        private void CloseRandom(object sender, EventArgs e)
        {
            RaffleView.IsVisible = false;
        }
    }
}