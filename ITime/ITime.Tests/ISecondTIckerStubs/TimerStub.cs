using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITime.Model.Infrastructure;

namespace ITime.Tests.ISecondTImerStubs
{
    class TimerStub : ITimer
    {
        public bool StartCalled { get; set; }
        public TimeSpan Interval { get; set; }

        public bool StopCalled { get; set; }

        public TimerStub()
        {
            StartCalled = false;
            StopCalled = false;
        }

        public void RaiseTick(int count = 1)
        {
            while (count-- > 0)
            {
                EventHandler handler = Tick;
                handler(this, EventArgs.Empty);
            }
        }

        #region ITimer Implementation

        public void Start(TimeSpan interval)
        {
            StartCalled = true;
            Interval = interval;
        }

        public void Stop()
        {
            StopCalled = true;
        }

        public event EventHandler Tick;

        #endregion
    }
}
