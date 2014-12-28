using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model.Interfaces;

namespace TimeToTomato.Tests.Mocks
{
    class SecondTicker : ISecondTicker
    {
        #region ISecondTicker Implementation

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public event EventHandler Tick;

        #endregion

        public event EventHandler StartCalled;
        public event EventHandler StopCalled;

        public void GenerateTicks(int count)
        {
            EventHandler handler = Tick;
            if (handler != null)
            {
                for (int i = 0; i < count; i++)
                    handler(this, EventArgs.Empty);
            }
        }
    }
}
