using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ITime.Model;

namespace ITime.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Assistant _assistant;
        private bool _isActive;
        private bool _isDone;

        private RelayCommand _startWorkCommand;
        private RelayCommand _startShortBreakCommand;
        private RelayCommand _startLongBreakCommand;
        private RelayCommand _stopCommand;

        public MainViewModel()
        {
            _assistant = new Assistant();
            _assistant.ElapsedChanged += _assistant_ElapsedChanged;
            _assistant.Started += _assistant_Started;
            _assistant.Stoped += _assistant_Stoped;
            _assistant.Done += _assistant_Done;
            _isActive = false;

            _startWorkCommand = new RelayCommand(_assistant.StartWorkTimer);
            _startShortBreakCommand = new RelayCommand(_assistant.StartShortBreak);
            _startLongBreakCommand = new RelayCommand(_assistant.StartLongBreak);
            _stopCommand = new RelayCommand(_assistant.StopTimer);
        }

        void _assistant_Done(object sender, EventArgs e)
        {
            IsDone = true;
        }

        void _assistant_Stoped(object sender, EventArgs e)
        {
            IsActive = false;
        }

        void _assistant_Started(object sender, EventArgs e)
        {
            IsDone = false;
            IsActive = true;
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    RaisePropertyChanged("IsActive");
                }
            }
        }

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone != value)
                {
                    _isDone = value;
                    RaisePropertyChanged("IsDone");
                }
            }
        }

        void _assistant_ElapsedChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("ElapsedSeconds");
        }

        public string ElapsedSeconds
        {
            get
            {
                return _assistant.TimeElapsed.ToString(@"mm\:ss\.fff");
            }
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
