using System.ComponentModel;

namespace mvvm
{
    public abstract class BaseINPC : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}