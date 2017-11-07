using KinderRegistartion.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderRegistartion
{
    public class RegistrationViewModel : ViewModelBase
    {
        public ObservableCollection<AttendeeViewModel> _attendeeList;
        public ObservableCollection<AttendeeViewModel> AttendeeList
        {
            get { return _attendeeList; }
            set { Set(ref _attendeeList, value); }
        }

        public RegistrationViewModel()
        {
            LoadRegisteredUsers();
        }

        private async void LoadRegisteredUsers()
        {
            Dialogs.ShowLoading("Loading Users...");
            IsBusy = true;

            var attendees = await App.Service.GetList(true);

            var list = new List<AttendeeViewModel>();
            foreach(var a in attendees)
            {
                var att = new AttendeeViewModel
                {
                    Id = a.Id,
                    FullName = $"{a.LastName}, {a.FirstName}",
                    Entity = a
                };
                list.Add(att);
            }
            AttendeeList = new ObservableCollection<AttendeeViewModel>(list.OrderBy(x=>x.FullName));

            IsBusy = false;

            Dialogs.HideLoading();

        }
    }

    public class AttendeeViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public KinderRegistration Entity { get; set; }
    }
}
