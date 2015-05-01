using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using ITime.Model.Infrastructure;

namespace ITime.Infrastructure
{
    class Timer : ITimer
    {
        DispatcherTimer _timer;

        public Timer()
        {
            _timer = new DispatcherTimer();
            
            _timer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            RaiseTick();
        }

        private void RaiseTick()
        {
            EventHandler h = Tick;
            if (h != null)
                h(this, EventArgs.Empty);
        }

        public void Start(TimeSpan interval)
        {
            _timer.Interval = interval;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public event EventHandler Tick;
    }
}
