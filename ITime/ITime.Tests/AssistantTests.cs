using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITime.Model;
using ITime.Model.Infrastructure;
using ITime.Tests.ISecondTImerStubs;

namespace ITime.Tests
{
    [TestFixture]
    public class AssistantTests
    {
        private static readonly TimeSpan WORK_TIME = new TimeSpan(0, 25, 0);
        private static readonly TimeSpan SHORT_BREAK = new TimeSpan(0, 5, 0);
        private static readonly TimeSpan LONG_BREAK = new TimeSpan(0, 20, 0);
        private static readonly TimeSpan TIMER_INTERVAL = new TimeSpan(0, 0, 1);

        private Assistant _assistant;

        private TimerStub _stubTicker;

        [SetUp]
        public void SetUp()
        {
            _stubTicker = new TimerStub();
            InfrastructureFactory.ProvideTimer(_stubTicker);

            _assistant = new Assistant();
        }

        [Test]
        public void Timer_Initial_ExistPropertyWithCorrectType()
        {
            ITimer t = _assistant.Timer;
            Assert.IsNotNull(t);
            Assert.IsInstanceOf<TimerStub>(t);
        }

        [Test]
        public void StartWorkTimer_Init_StartIsCalled()
        {
            _assistant.StartWorkTimer();

            Assert.True(_stubTicker.StartCalled);
        }

        [Test]
        public void StartWorkTimer_Init_WorkTimeElapsed()
        {
            _assistant.StartWorkTimer();

            Assert.AreEqual(WORK_TIME, _assistant.TimeElapsed);
        }

        [Test]
        public void StartWorkTimer_Init_SecondsUpdatePassed()
        {
            _assistant.StartWorkTimer();

            Assert.AreEqual(TIMER_INTERVAL, _stubTicker.Interval);
        }

        [Test]
        public void StartShortBreak_Init_ShortBreakSecondsElapsed()
        {
            _assistant.StartShortBreak();

            Assert.AreEqual(SHORT_BREAK, _assistant.TimeElapsed);
        }

        [Test]
        public void StartShortBreak_Init_SecondsUpdatePassed()
        {
            _assistant.StartShortBreak();

            Assert.AreEqual(TIMER_INTERVAL, _stubTicker.Interval);
        }

        [Test]
        public void StartLongBreak_Init_TimerStartCalled()
        {
            _assistant.StartLongBreak();

            Assert.True(_stubTicker.StartCalled);
        }

        [Test]
        public void StartLongBreak_Init_TimerLongBreakSecondsPassed()
        {
            _assistant.StartLongBreak();

            Assert.AreEqual(LONG_BREAK, _assistant.TimeElapsed);
        }

        [Test]
        public void StartLongBreak_Init_TimerSecondsUpdatePassed()
        {
            _assistant.StartLongBreak();

            Assert.AreEqual(TIMER_INTERVAL, _stubTicker.Interval);
        }

        [Test]
        public void Stop_Init_CallStopToTimer()
        {
            _assistant.StopTimer();

            Assert.True(_stubTicker.StopCalled);
        }

        [Test]
        public void ElapsedChanged_OnTimerTicks_RaiseEvent()
        {
            bool raised = false;
            _assistant.ElapsedChanged += (s, e) => raised = true;
            Thread.Sleep(1000);
            _stubTicker.RaiseTick();

            Assert.True(raised);
        }
        [Test]
        public void Stop_CallStop_StoppedRaised()
        {
            bool raised = false;
            _assistant.Stoped += (sender, e) => raised = true;
            _assistant.StopTimer();
            Assert.IsTrue(raised);
        }

        [Test]
        public void StartWorkTime_SetUpWorkTime_CustomTimeElapsed()
        {
            var wt = new TimeSpan(0, 0, 1);
            _assistant.WorkTime = wt;
            _assistant.StartWorkTimer();

            Assert.AreEqual(wt, _assistant.TimeElapsed);
        }

        [Test]
        public void StartShortBreak_SetUpShortBreak_CustomTimeElapsed()
        {
            var sb = new TimeSpan(0, 0, 1);
            _assistant.ShortBreakTime = sb;

            _assistant.StartShortBreak();

            Assert.AreEqual(sb, _assistant.TimeElapsed);
        }

        [Test]
        public void StartLongBreak_SetUpLongBreak_CustomTimeElapsed()
        {
            var sb = new TimeSpan(0, 0, 1);
            _assistant.LongBreakTime = sb;

            _assistant.StartLongBreak();

            Assert.AreEqual(sb, _assistant.TimeElapsed);
        }

        [Test]
        public void StartWorkTimer_SetUpZeroTime_RaiseDoneAfterTimerTick()
        {
            bool doneRaised = false;

            _assistant.WorkTime = TimeSpan.Zero;
            _assistant.Done += (s, e) => doneRaised = true;
            _assistant.StartWorkTimer();
            _stubTicker.RaiseTick();
            Assert.True(doneRaised);
        }
    }
}
