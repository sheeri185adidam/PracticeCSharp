using System.ComponentModel;

namespace mvvm
{
    public class ContactModel : INotifyPropertyChanged
    {
        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged("FirstName");
                RaisePropertyChanged("FullName");
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged("LastName");
                RaisePropertyChanged("FullName");
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            return obj is ContactModel model && model.FullName.Equals(FullName);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }
    }
}