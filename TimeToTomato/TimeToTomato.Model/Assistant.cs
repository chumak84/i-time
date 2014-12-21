using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeToTomato.Model
{
    public class Assistant
    {
        public event EventHandler TimerActivated;
        public event EventHandler TimerStopped;

        public void StartWorkTimer()
        {
            EventHandler handler = TimerActivated;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void StopTimer()
        {
            EventHandler handler = TimerStopped;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
