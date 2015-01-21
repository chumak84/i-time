using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model.Infrastructure;

namespace TimeToTomato.Model
{
    public class Timer
    {
        private ISecondTicker _ticker;
        private bool _isActive;
        private int _secondsElapsed;

        public event EventHandler SecondsElapsedChanged;
        public event EventHandler IsActiveChanged;

        public Timer(ISecondTicker ticker)
        {
            _ticker = ticker;
            _ticker.Tick += _ticker_Tick;
        }

        public int SecondsElapsed
        {
            get { return _secondsElapsed; }
            set
            {
                if(_secondsElapsed != value)
                {
                    _secondsElapsed = value;
                    RaiseSecondsElapsedChanged();
                }
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            private set
            {
                if(_isActive != value)
                {
                    _isActive = value;
                    EventHandler handler = IsActiveChanged;
                    if (handler != null)
                        handler(this, EventArgs.Empty);
                }
            }
        }

        void _ticker_Tick(object sender, EventArgs e)
        {
            SecondsElapsed--;
            RaiseSecondsElapsedChanged();
        }

        private void RaiseSecondsElapsedChanged()
        {
            EventHandler handler = SecondsElapsedChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void Start()
        {
            if (SecondsElapsed > 0)
            {
                _ticker.Start();
                IsActive = true;
            }
        }

        public void Stop()
        {
            if (SecondsElapsed > 0)
            {
                _ticker.Stop();
                IsActive = false;
            }
        }
    }
}
