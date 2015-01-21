using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeToTomato.Model.Infrastructure
{
    public class InfrastructureFactory
    {
        static Func<ISecondTicker> _factoryFunc;

        public static ISecondTicker CreateSecondTicker()
        {
            if (_factoryFunc == null)
                throw new NullReferenceException("ISecondTicker implementation is not provided");

            return _factoryFunc();
        }

        public static void ProvideSecondTicker<T>() where T : ISecondTicker, new()
        {
            _factoryFunc = () => new T();
        }

        public static void ProvideSecondTicker(ISecondTicker _tickerInstance)
        {
            _factoryFunc = () => _tickerInstance;
        }
    }
}
