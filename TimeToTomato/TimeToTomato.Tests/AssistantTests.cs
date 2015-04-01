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
        private const int WORK_SECONDS = 25 * 60;
        private const int SHORTBREAK_SECONDS = 5 * 60;
        private const int LONGBREAK_SECONDS = 20 * 60;
        private const int UPDATE_SECONDS = 1;

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
        public void StartWorkTimer_Init_WorkTimerSecondsPassed()
        {
            _assistant.StartWorkTimer();

            Assert.AreEqual(WORK_SECONDS, _stubTicker.TimeInSecondsPassed);
        }

        [Test]
        public void StartWorkTimer_Init_SecondsUpdatePassed()
        {
            _assistant.StartWorkTimer();

            Assert.AreEqual(UPDATE_SECONDS, _stubTicker.SecondsUpdatePassed);
        }

        [Test]
        public void StartShortBreak_Init_ShortBreakSecondsPassed()
        {
            _assistant.StartShortBreak();

            Assert.AreEqual(SHORTBREAK_SECONDS, _stubTicker.TimeInSecondsPassed);
        }

        [Test]
        public void StartShortBreak_Init_SecondsUpdatePassed()
        {
            _assistant.StartShortBreak();

            Assert.AreEqual(UPDATE_SECONDS, _stubTicker.SecondsUpdatePassed);
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

            Assert.AreEqual(LONGBREAK_SECONDS, _stubTicker.TimeInSecondsPassed);
        }

        [Test]
        public void StartLongBreak_Init_TimerSecondsUpdatePassed()
        {
            _assistant.StartLongBreak();

            Assert.AreEqual(UPDATE_SECONDS, _stubTicker.SecondsUpdatePassed);
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

            _stubTicker.SecondsElapsedReturn--;
            _stubTicker.RaiseTick();

            Assert.True(raised);
        }

        [Test]
        public void Stoped_TimerDoneRaise_StopedRaised()
        {
            bool raised = false;
            _assistant.Stoped += (s, e) => raised = true;

            _stubTicker.RaiseDone();

            Assert.True(raised);
        }
    }
}
