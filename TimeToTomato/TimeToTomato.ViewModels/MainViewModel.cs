using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeToTomato.Model;

namespace TimeToTomato.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Assistant _assistant;
        private bool _isActive;

        private RelayCommand _startWorkCommand;
        private RelayCommand _startShortBreakCommand;
        private RelayCommand _startLongBreakCommand;
        private RelayCommand _stopCommand;

        public MainViewModel()
        {
            _assistant = new Assistant();
            _assistant.ElapsedChanged += _assistant_ElapsedChanged;
            _assistant.Started += (s, e) => IsActive = true;
            _assistant.Stoped += (s, e) => IsActive = false;
            _isActive = false;

            _startWorkCommand = new RelayCommand(_assistant.StartWorkTimer);
            _startShortBreakCommand = new RelayCommand(_assistant.StartShortBreak);
            _startLongBreakCommand = new RelayCommand(_assistant.StartLongBreak);
            _stopCommand = new RelayCommand(_assistant.StopTimer);
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if(_isActive != value)
                {
                    _isActive = value;
                    RaisePropertyChanged("IsActive");
                }
            }
        }

        void _assistant_ElapsedChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("ElapsedSeconds");
        }

        public string ElapsedSeconds
        {
            get { return _assistant.TimeElapsed.ToString(@"mm\:ss\.fff"); }
        }

        public ICommand StartWorkCommand
        {
            get { return _startWorkCommand; }
        }

        public ICommand StartShortBreakCommand
        {
            get { return _startShortBreakCommand; }
        }

        public ICommand StartLongBreakCommand
        {
            get { return _startLongBreakCommand; }
        }

        public ICommand StopCommand
        {
            get { return _stopCommand; }
        }
    }
}
