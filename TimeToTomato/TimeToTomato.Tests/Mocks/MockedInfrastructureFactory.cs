using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model.Infrastructure;

namespace TimeToTomato.Tests.Mocks
{
    class MockedInfrastructureFactory : InfrastructureFactory
    {
        public void Init()
        {
            InfrastructureFactory.Factory = this;
        }

        public SecondTicker SecondTicker { get; set; }

        protected override ISecondTicker DoCreateSecondTicker()
        {
            return SecondTicker;
        }
    }
}
