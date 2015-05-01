using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITime.Model.Infrastructure
{
    public interface ITimer
    {
        void Start(TimeSpan interval);
        void Stop();
        event EventHandler Tick;
    }
}
