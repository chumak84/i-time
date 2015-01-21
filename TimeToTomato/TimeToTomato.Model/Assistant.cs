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
        private Timer _timer;
        private const int WORK_SECONDS = 25 * 60;
        private const int SHORTBREAK_SECONDS = 5 * 60;
        private const int LONGBREAK_SECONDS = 20 * 60;

        public Assistant()
        {
            _timer = new Timer(InfrastructureFactory.CreateSecondTicker());
        }

        public Timer Timer
        {
            get { return _timer; }
        }

        public void StartWorkTimer()
        {
            _timer.SecondsElapsed = WORK_SECONDS;
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public void StartShortBreak()
        {
            _timer.SecondsElapsed = SHORTBREAK_SECONDS;
            _timer.Start();
        }
    }
}
