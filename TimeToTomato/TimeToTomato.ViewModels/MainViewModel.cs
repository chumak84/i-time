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

        private RelayCommand _startWorkCommand;
        private RelayCommand _startShortBreakCommand;
        private RelayCommand _startLongBreakCommand;
        private RelayCommand _stopCommand;

        public MainViewModel()
        {
            _assistant = new Assistant();
            _assistant.ElapsedChanged += _assistant_ElapsedChanged;

            _startWorkCommand = new RelayCommand(_assistant.StartWorkTimer);
            _startShortBreakCommand = new RelayCommand(_assistant.StartShortBreak);
            _startLongBreakCommand = new RelayCommand(_assistant.StartLongBreak);
            _stopCommand = new RelayCommand(_assistant.StopTimer);
        }

        void _assistant_ElapsedChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("ElapsedSeconds");
        }

        public int ElapsedSeconds
        {
            get { return _assistant.ElapsedSeconds; }
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
