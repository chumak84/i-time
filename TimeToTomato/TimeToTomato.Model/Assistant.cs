using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model.Interfaces;

namespace TimeToTomato.Model
{
    public class Assistant
    {
        private ISecondTicker _mockTicker;
        private int _secondsElapsed = 60 * 25;

        public event EventHandler TimerActivated;
        public event EventHandler TimerStopped;
        public event EventHandler SecondsElapsedChanged;

        public Assistant(ISecondTicker _mockTicker)
        {
            // TODO: Complete member initialization
            this._mockTicker = _mockTicker;
            this._mockTicker.Tick += _mockTicker_Tick;
        }

        void _mockTicker_Tick(object sender, EventArgs e)
        {
            SecondsElapsed--;
        }

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

            SecondsElapsed = 60 * 25;
        }

        public int SecondsElapsed
        {
            get { return _secondsElapsed; }
            set
            { 
                if(_secondsElapsed != value)
                {
                    _secondsElapsed = value;
                    EventHandler handler = SecondsElapsedChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }
    }
}
