using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TimeToTomato.Model.Infrastructure;

namespace TimeToTomato.Infrastructure
{
    class Timer : ITimer
    {
        DispatcherTimer _timer;

        DateTime _end;

        public Timer()
        {
            _timer = new DispatcherTimer();

            _timer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            RaiseTick();

            if (SecondsElapsed <= 0)
            {
                _timer.Stop();
                RaiseDone();
            }
        }

        private void RaiseDone()
        {
            EventHandler h = Done;
            if (h != null)
                h(this, EventArgs.Empty);
        }

        private void RaiseTick()
        {
            EventHandler h = Tick;
            if (h != null)
                h(this, EventArgs.Empty);
        }

        public void Start(int timeInSeconds, int secondsUpdate)
        {
            _timer.Interval = new TimeSpan(0, 0, secondsUpdate);
            _end = DateTime.Now.AddSeconds(timeInSeconds);
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public int SecondsElapsed
        {
            get
            {
                var elapsed = _end - DateTime.Now;
                return (int)elapsed.TotalSeconds;
            }
        }

        public event EventHandler Tick;

        public event EventHandler Done;
    }
}
