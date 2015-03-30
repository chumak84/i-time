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
        private ITimer _timer;
        private const int WORK_SECONDS = 25 * 60;
        private const int SHORTBREAK_SECONDS = 5 * 60;
        private const int LONGBREAK_SECONDS = 20 * 60;
        private const int UPDATE_SECONDS = 1;

        public Assistant()
        {
            _timer = InfrastructureFactory.CreateTimer();
        }

        public ITimer Timer
        {
            get { return _timer; }
        }

        public void StartWorkTimer()
        {
            _timer.Start(WORK_SECONDS, UPDATE_SECONDS);
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        public void StartShortBreak()
        {
            _timer.Start(SHORTBREAK_SECONDS, UPDATE_SECONDS);
        }

        public void StartLongBreak()
        {
            _timer.Start(LONGBREAK_SECONDS, UPDATE_SECONDS);
        }
    }
}
