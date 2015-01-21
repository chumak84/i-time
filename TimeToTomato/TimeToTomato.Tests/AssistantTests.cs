using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model;
using TimeToTomato.Model.Infrastructure;
using TimeToTomato.Tests.ISecondTImerStubs;

namespace TimeToTomato.Tests
{
    [TestFixture]
    public class AssistantTests
    {
        private const int workTimerSeconds = 25 * 60;
        private const int shortBreakSeconds = 5 * 60;
        private const int longBreakSeconds = 20 * 60;

        private Assistant _assistant;

        private SecondTickerStub _mockTicker;

        [SetUp]
        public void SetUp()
        {
            _mockTicker = new SecondTickerStub();
            InfrastructureFactory.ProvideSecondTicker(_mockTicker);

            _assistant = new Assistant();
        }

        [Test]
        public void Timer_Initial_ExistPropertyWithCorrectType()
        {
            Timer t = _assistant.Timer;
            Assert.IsNotNull(t);
        }

        [Test]
        public void StartWorkTimer_Start_WorkSecondsPassedToTimer()
        {
            _assistant.StartWorkTimer();

            Timer t = _assistant.Timer;
            Assert.AreEqual(workTimerSeconds, t.SecondsElapsed);
        }

        [Test]
        public void StartWorkTimer_Start_TimerIsActiveTrue()
        {
            _assistant.StartWorkTimer();

            Timer t = _assistant.Timer;
            Assert.True(t.IsActive);
        }

        [Test]
        public void StopTimer_StopAfterStart_TimerIsActiveFalse()
        {
            _assistant.StartWorkTimer();
            _assistant.StopTimer();

            Timer t = _assistant.Timer;
            Assert.False(t.IsActive);
        }

        [Test]
        public void StartShortBreak_TimerStopped_ElapsedShortBreakSeconds()
        {
            _assistant.StartShortBreak();

            Timer t = _assistant.Timer;
            Assert.AreEqual(shortBreakSeconds, t.SecondsElapsed);
        }

        [Test]
        public void StartShortBreak_TimerStopped_TimerIsActive()
        {
            _assistant.StartShortBreak();

            Timer t = _assistant.Timer;
            Assert.True(t.IsActive);
        }

        [Test]
        public void StartLongBreak_TimerStopped_ElapsedLongBreakSeconds()
        {
            _assistant.StartShortBreak();

            Timer t = _assistant.Timer;
            Assert.AreEqual(shortBreakSeconds, t.SecondsElapsed);
        }

        [Test]
        public void StartLongBreak_TimerStopped_TimerIsActive()
        {
            _assistant.StartShortBreak();

            Timer t = _assistant.Timer;
            Assert.True(t.IsActive);
        }
    }
}
