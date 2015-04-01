using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model.Infrastructure;

namespace TimeToTomato.Model
{
    public class Assistant
    {
        private ITimer _timer;
        private const int WORK_SECONDS = 25 * 60;
        private const int SHORTBREAK_SECONDS = 5 * 60;
        private const int LONGBREAK_SECONDS = 20 * 60;
        private const int UPDATE_SECONDS = 1;

        private int _elapsedSeconds = 0;

        public event EventHandler ElapsedChanged;
        public event EventHandler Started;
        public event EventHandler Stoped;

        public Assistant()
        {
            _timer = InfrastructureFactory.CreateTimer();
            _elapsedSeconds = _timer.SecondsElapsed;
            _timer.Tick += _timer_Tick;
            _timer.Done += _timer_Done;
        }

        void _timer_Done(object sender, EventArgs e)
        {
            RaiseStoped();
        }

        private void RaiseStoped()
        {
            EventHandler handler = Stoped;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void RaiseStarted()
        {
            EventHandler handler = Started;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            ElapsedSeconds = _timer.SecondsElapsed;
        }

        public ITimer Timer
        {
            get { return _timer; }
        }

        public int ElapsedSeconds
        {
            get { return _elapsedSeconds; }
            set
            {
                if(_elapsedSeconds != value)
                {
                    _elapsedSeconds = value;
                    EventHandler handler = ElapsedChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        public void StartWorkTimer()
        {
            _timer.Start(WORK_SECONDS, UPDATE_SECONDS);
            ElapsedSeconds = _timer.SecondsElapsed;
            RaiseStarted();
        }

        public void StopTimer()
        {
            _timer.Stop();
            ElapsedSeconds = 0;
        }

        public void StartShortBreak()
        {
            _timer.Start(SHORTBREAK_SECONDS, UPDATE_SECONDS);
            ElapsedSeconds = _timer.SecondsElapsed;
            RaiseStarted();
        }

        public void StartLongBreak()
        {
            _timer.Start(LONGBREAK_SECONDS, UPDATE_SECONDS);
            ElapsedSeconds = _timer.SecondsElapsed;
            RaiseStarted();
        }
    }
}
