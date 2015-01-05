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
        private ISecondTicker _mockTicker;
        private int _secondsElapsed = 60 * 25;
        private bool _isActive = false;

        public event EventHandler TimerActivated;
        public event EventHandler TimerStopped;
        public event EventHandler SecondsElapsedChanged;

        public Assistant()
        {
            this._mockTicker = InfrastructureFactory.CreateSecondTicker();
            this._mockTicker.Tick += _mockTicker_Tick;
        }

        void _mockTicker_Tick(object sender, EventArgs e)
        {
            SecondsElapsed--;
            if(SecondsElapsed == 0)
            {
                StopTimer();
            }
        }

        public void StartWorkTimer()
        {
            EventHandler handler = TimerActivated;
            if (handler != null)
                handler(this, EventArgs.Empty);

            _isActive = true;
        }

        public void StopTimer()
        {
            EventHandler handler = TimerStopped;
            if (handler != null)
                handler(this, EventArgs.Empty);

            SecondsElapsed = 60 * 25;
            _isActive = false;
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

        public bool IsActive
        {
            get { return _isActive; }
        }
    }
}
