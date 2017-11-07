using Acr.UserDialogs;
using KinderRegistartion.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KinderRegistartion
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void Set<T>(ref T field, T newValue = default(T), [CallerMemberName]string propertyName = null)
        {
            if (field != null)
            {
                if (!field.Equals(newValue))
                {
                    field = newValue;
                    OnPropertyChanged(propertyName);
                }
            }
            else
            {
                if (newValue != null)
                {
                    field = newValue;
                    OnPropertyChanged(propertyName);
                }
            }

        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NavigationPage Navigation { get; set; }
        public IUserDialogs Dialogs { get; set; } = UserDialogs.Instance;

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }
        
    }
}
