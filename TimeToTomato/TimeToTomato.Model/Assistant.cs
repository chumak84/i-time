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
        private static readonly TimeSpan WORK_TIME = new TimeSpan(0, 25, 0);
        private static readonly TimeSpan SHORT_BREAK = new TimeSpan(0, 5, 0);
        private static readonly TimeSpan LONG_BREAK = new TimeSpan(0, 20, 0);
        private static readonly TimeSpan TIMER_INTERVAL = new TimeSpan(0, 0, 1);

        private DateTime _end;
        private TimeSpan _timeElapsed;

        public event EventHandler ElapsedChanged;
        public event EventHandler Started;
        public event EventHandler Stoped;

        public Assistant()
        {
            _timeElapsed = new TimeSpan();
            _timer = InfrastructureFactory.CreateTimer();
            _timer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            _timeElapsed = _end - DateTime.Now;
            if(_timeElapsed < TimeSpan.Zero)
            {
                _timeElapsed = TimeSpan.Zero;
                _timer.Stop();
                RaiseStoped();
            }

            RaiseElapsedChanged();
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

        private void RaiseElapsedChanged()
        {
            EventHandler handler = ElapsedChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public ITimer Timer
        {
            get { return _timer; }
        }

        public TimeSpan TimeElapsed
        {
            get { return _timeElapsed; }
        }

        public void StartWorkTimer()
        {
            _end = DateTime.Now + WORK_TIME;
            UpdateState();
            _timer.Start(TIMER_INTERVAL);
            RaiseStarted();
        }

        public void StopTimer()
        {
            _timer.Stop();
            _timeElapsed = TimeSpan.Zero;
            RaiseElapsedChanged();
        }

        public void StartShortBreak()
        {
            _end = DateTime.Now + SHORT_BREAK;
            UpdateState();
            _timer.Start(TIMER_INTERVAL);
            RaiseStarted();
        }

        public void StartLongBreak()
        {
            _end = DateTime.Now + LONG_BREAK;
            UpdateState();
            _timer.Start(TIMER_INTERVAL);
            RaiseStarted();
        }
    }
}
