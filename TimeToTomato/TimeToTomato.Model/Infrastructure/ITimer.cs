using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeToTomato.Model.Infrastructure
{
    public interface ITimer
    {
        void Start(int timeInSeconds, int secondsUpdate);
        void Stop();
        int SecondsElapsed { get; }
        bool IsActive { get; }
        event EventHandler Tick;
        event EventHandler Done;
    }
}
