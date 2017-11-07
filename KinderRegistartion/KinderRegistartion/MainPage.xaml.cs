using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KinderRegistartion
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void Entry_Completed(object sender, EventArgs e)
        {
            var cur = sender as Entry;
            var stack = cur.Parent as StackLayout;
            var curIndex = stack.Children.IndexOf(cur);
            var nextControl = stack.Children[curIndex + 1];
            nextControl.Focus();
        }

        private void Raffle_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RafflePage());
        }
    }
}
