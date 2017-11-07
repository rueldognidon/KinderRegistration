using KinderRegistartion.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KinderRegistartion
{
    public class MainViewModel : ViewModelBase
    {
        private Command gotoRegisteredCommand;
        public ICommand GotoRegisteredCommand => gotoRegisteredCommand;


        private Command submitCommand;
        public ICommand SubmitCommand => submitCommand;

        private Command clearCommand;
        public ICommand ClearCommand => clearCommand;

        public KinderRegistration Attendee { get; set; }

        private bool _isNotRegistered = true;

        public bool IsNotRegistered
        {
            get { return _isNotRegistered; }
            set { Set(ref _isNotRegistered, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { Set(ref _firstName, value); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { Set(ref _lastName, value); }
        }
        private string _companySchool;

        public string CompanySchool
        {
            get { return _companySchool; }
            set { Set(ref _companySchool, value); }
        }

        private string _position;

        public string Position
        {
            get { return _position; }
            set { Set(ref _position, value); }
        }

        private string _email;

        public string EmailAddress
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        private string _mobileNumber;

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { Set(ref _mobileNumber, value); }
        }

        private string _years;

        public string Years
        {
            get { return _years; }
            set { Set(ref _years, value); }
        }

        private bool _willing;

        public bool Willing
        {
            get { return _willing; }
            set { Set(ref _willing, value); }
        }



        public MainViewModel()
        {
            gotoRegisteredCommand = new Command(GotoRegistered, CanExecute);
            submitCommand = new Command(Submit, CanExecute);
            clearCommand = new Command(Clear, CanExecute);


            MessagingCenter.Subscribe<object, KinderRegistration>(this, "RegisteredLoaded", RegisteredLoaded);
        }

        private void Clear(object obj)
        {
            FirstName = LastName = CompanySchool = Position = EmailAddress = MobileNumber = Years = "";
            Willing = false;
            IsNotRegistered = true;
            Attendee = null;
        }

        private async void Submit(object obj)
        {

            if(string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                await Dialogs.AlertAsync("Please enter First Name and Last Name.");
                return;
            }

            Dialogs.ShowLoading("Submitting...");
            if (Attendee == null)
            {
                Attendee = new KinderRegistration();
                
            }
            Attendee.FirstName = FirstName;
            Attendee.LastName = LastName;
            Attendee.CompanySchool = CompanySchool;
            Attendee.Position = Position;
            Attendee.EmailAddress = EmailAddress;
            Attendee.TimeIn = DateTime.Now;
            Attendee.MobileNumber = MobileNumber;
            Attendee.WillingToBeContacted = Willing;
            Attendee.YearsOfExperience = Years;

            if (string.IsNullOrEmpty(Attendee.Id))
                await App.Service.AddItem(Attendee);
            else
                await App.Service.UpdateItem(Attendee);

            Clear(null);

            Dialogs.HideLoading();
            await Dialogs.AlertAsync("Registration Complete!");
        }

        private void RegisteredLoaded(object sender, KinderRegistration attendee)
        {
            this.Attendee = attendee;
            FirstName = attendee.FirstName;
            LastName = attendee.LastName;
            CompanySchool = attendee.CompanySchool;
            Position = attendee.Position;
            EmailAddress = attendee.EmailAddress;
            Willing = attendee.WillingToBeContacted;
            Years = attendee.YearsOfExperience;
            MobileNumber = attendee.MobileNumber;
            IsNotRegistered = false;
        }

        private bool CanExecute(object arg)
        {
            return !IsBusy;
        }

        private void GotoRegistered(object arg)
        {
            IsBusy = true;

            Navigation.PushAsync(new RegisteredPage());

            IsBusy = false;

        }
    }
}
