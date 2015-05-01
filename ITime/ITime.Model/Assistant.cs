using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITime.Model.Infrastructure;

namespace ITime.Model
{
    public class Assistant
    {
        private ITimer _timer;
        public static readonly TimeSpan DEFAULT_WORK_TIME = new TimeSpan(0, 25, 0);
        public static readonly TimeSpan DEFAULT_SHORT_BREAK = new TimeSpan(0, 5, 0);
        public static readonly TimeSpan DEFAULT_LONG_BREAK = new TimeSpan(0, 20, 0);
        public static readonly TimeSpan DEFAULT_TIMER_INTERVAL = new TimeSpan(0, 0, 1);

        private DateTime _end;
        private TimeSpan _timeElapsed;

        public event EventHandler ElapsedChanged;
        public event EventHandler Started;
        public event EventHandler Stoped;
        public event EventHandler Done;

        public Assistant()
        {
            _timeElapsed = TimeSpan.Zero;
            _timer = InfrastructureFactory.CreateTimer();
            _timer.Tick += _timer_Tick;

            this.WorkTime = DEFAULT_WORK_TIME;
            this.ShortBreakTime = DEFAULT_SHORT_BREAK;
            this.LongBreakTime = DEFAULT_LONG_BREAK;
        }

        public TimeSpan WorkTime { get; set; }
        public TimeSpan ShortBreakTime { get; set; }
        public TimeSpan LongBreakTime { get; set; }

        private void _timer_Tick(object sender, EventArgs e)
        {
            UpdateState();
            CheckDone();
        }

        private void UpdateState()
        {
            TimeElapsed = _end - DateTime.Now;
            if (_timeElapsed == TimeSpan.Zero)
            {
                _timer.Stop();
                RaiseStoped();
            }
        }

        private void CheckDone()
        {
            if (_timeElapsed == TimeSpan.Zero)
                RaiseDone();
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

        private void RaiseDone()
        {
            EventHandler handler = Done;
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
            private set
            {
                if (value < TimeSpan.Zero)
                    _timeElapsed = TimeSpan.Zero;
                else _timeElapsed = value;
                RaiseElapsedChanged();
            }
        }

        public void StartWorkTimer()
        {
            _end = DateTime.Now + this.WorkTime;
            TimeElapsed = this.WorkTime;
            _timer.Start(DEFAULT_TIMER_INTERVAL);
            RaiseStarted();
        }

        public void StopTimer()
        {
            _timer.Stop();
            TimeElapsed = TimeSpan.Zero;
            RaiseStoped();
        }

        public void StartShortBreak()
        {
            _end = DateTime.Now + ShortBreakTime;
            TimeElapsed = ShortBreakTime;

            _timer.Start(DEFAULT_TIMER_INTERVAL);
            RaiseStarted();
        }

        public void StartLongBreak()
        {
            _end = DateTime.Now + LongBreakTime;
            TimeElapsed = LongBreakTime;

            _timer.Start(DEFAULT_TIMER_INTERVAL);
            RaiseStarted();
        }
    }
}
