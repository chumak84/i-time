using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeToTomato.Model.Infrastructure
{
    public class InfrastructureFactory
    {
        static Func<ITimer> _factoryFunc;

        public static ITimer CreateTimer()
        {
            if (_factoryFunc == null)
                throw new NullReferenceException("ISecondTicker implementation is not provided");

            return _factoryFunc();
        }

        public static void ProvideTimer<T>() where T : ITimer, new()
        {
            _factoryFunc = () => new T();
        }

        public static void ProvideTimer(ITimer _tickerInstance)
        {
            _factoryFunc = () => _tickerInstance;
        }
    }
}
