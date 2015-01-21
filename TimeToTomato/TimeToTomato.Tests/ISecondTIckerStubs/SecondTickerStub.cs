using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model.Infrastructure;

namespace TimeToTomato.Tests.ISecondTImerStubs
{
    class SecondTickerStub : ISecondTicker
    {
        public bool StartCalled { get; set; }
        public bool StopCalled { get; set; }

        public SecondTickerStub()
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

        #region ISecondTicker Implementation

        public void Start()
        {
            StartCalled = true;
        }

        public void Stop()
        {
            StopCalled = true;
        }

        public event EventHandler Tick;

        #endregion

    }
}
