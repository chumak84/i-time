using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model.Infrastructure;

namespace TimeToTomato.Tests.ISecondTImerStubs
{
    class TimerStub : ITimer
    {
        public bool StartCalled { get; set; }
        public int TimeInSecondsPassed { get; set; }
        public int SecondsUpdatePassed { get; set; }

        public bool StopCalled { get; set; }

        public int SecondsElapsedReturn { get; set; }

        public bool IsActiveReturn { get; set; }

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

        public void RaiseDone()
        {
            EventHandler handler = Done;
            handler(this, EventArgs.Empty);
        }

        #region ITimer Implementation

        public void Start(int timeInSeconds, int secondsUpdate)
        {
            StartCalled = true;
            TimeInSecondsPassed = timeInSeconds;
            SecondsUpdatePassed = secondsUpdate;
        }

        public void Stop()
        {
            StopCalled = true;
        }

        public int SecondsElapsed
        {
            get { return SecondsElapsedReturn; }
        }

        public bool IsActive
        {
            get { return IsActiveReturn; }
        }

        public event EventHandler Done;

        public event EventHandler Tick;

        #endregion
    }
}
