using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderRegistartion
{
    public class RaffleViewModel : ViewModelBase
    {

        public ObservableCollection<AttendeeViewModel> _attendeeList;
        public ObservableCollection<AttendeeViewModel> AttendeeList
        {
            get { return _attendeeList; }
            set { Set(ref _attendeeList, value); }
        }

        public RaffleViewModel()
        {
            LoadRegisteredUsers();
        }

        private async void LoadRegisteredUsers()
        {
            Dialogs.ShowLoading("Loading Users...");
            IsBusy = true;

            var attendees = await App.Service.GetList(true);
            
            var list = new List<AttendeeViewModel>();
            foreach (var a in attendees)
            {
                var att = new AttendeeViewModel
                {
                    Id = a.Id,
                    FullName = $"{a.LastName}, {a.FirstName}",
                    Entity = a
                };
                if (!a.TimeIn.Equals(default(DateTime)))
                {
                    var dup = list.Find(x => x.FullName.ToLower().Equals(att.FullName.ToLower()));

                    if (dup == null)
                        list.Add(att);
                }
            }
            AttendeeList = new ObservableCollection<AttendeeViewModel>(list.OrderBy(x => x.FullName));

            IsBusy = false;

            Dialogs.HideLoading();

        }
    }
}
