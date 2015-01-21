using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TimeToTomato.Model;
using TimeToTomato.Model.Infrastructure;
using TimeToTomato.Tests.ISecondTImerStubs;

namespace TimeToTomato.Tests
{
    [TestFixture]
    class TimerTests
    {
        private SecondTickerStub _ticker;
        private Timer _timer;

        [SetUp]
        public void SetUp()
        {
            _ticker = new SecondTickerStub();

            _timer = new Timer(_ticker);
        }

        [Test]
        public void SecondsElapsed_Initial()
        {
            int seconds = _timer.SecondsElapsed;
            Assert.AreEqual(seconds, default(int));
        }

        [Test]
        public void SecondsElapsed_Stored()
        {
            int someValue = 20;
            _timer.SecondsElapsed = someValue;

            Assert.AreEqual(someValue, _timer.SecondsElapsed);
        }

        [Test]
        public void SecondsElapsed_TickerTicks_SecondsChanging()
        {
            _timer.SecondsElapsed = 25;

            _ticker.RaiseTick();

            var actual = _timer.SecondsElapsed;
            var expected = 25 - 1;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SecondsElapsedChanged_TickerTicks_EventRaised()
        {
            bool raised = false;
            _timer.SecondsElapsedChanged += (o, e) => raised = true;

            _ticker.RaiseTick();

            Assert.True(raised);
        }

        [Test]
        public void SecondsElapsedChanged_SecondsChanged_EventRaised()
        {
            _timer.SecondsElapsed = 0;
            bool raised = false;

            _timer.SecondsElapsedChanged += (o, e) => raised = true;
            _timer.SecondsElapsed = 25;

            Assert.True(raised);
        }

        [Test]
        public void Start_SecondsElapsedMoreZero_TickerIsStarted()
        {
            _timer.SecondsElapsed = 25;

            _timer.Start();

            Assert.True(_ticker.StartCalled);
        }

        [Test]
        public void Start_SecondsElapsedIsZero_TickerStartIsNotCalled()
        {
            _timer.SecondsElapsed = 0;

            _timer.Start();

            Assert.False(_ticker.StartCalled);
        }

        [Test]
        public void Stop_SecondElapsedMoreZero_TickerStopIsCalled()
        {
            _timer.SecondsElapsed = 1;

            _timer.Stop();

            Assert.True(_ticker.StopCalled);
        }

        [Test]
        public void Stop_SecondsElapsedEqualZero_TickerStopIsNotCalled()
        {
            _timer.SecondsElapsed = 0;

            _timer.Stop();

            Assert.False(_ticker.StopCalled);
        }

        [Test]
        public void IsActive_SecondsMoreZeroStartCalled_IsActiveTrue()
        {
            _timer.SecondsElapsed = 25;
            _timer.Start();

            Assert.True(_timer.IsActive);
        }
        [Test]
        public void IsActive_Initial_IsFalse()
        {
            Assert.False(_timer.IsActive);
        }

        [Test]
        public void IsActive_StartThenStop_IsFalse()
        {
            _timer.SecondsElapsed = 25;

            _timer.Start();
            _timer.Stop();

            Assert.False(_timer.IsActive);
        }

        [Test]
        public void IsActiveChanged_SecondsMoreZeroStartCalled_Raised()
        {
            bool raised = false;
            _timer.IsActiveChanged += (o, e) => raised = true;

            _timer.SecondsElapsed = 25;
            _timer.Start();

            Assert.True(raised);
        }
    }
}
