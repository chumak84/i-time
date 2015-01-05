using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model;
using TimeToTomato.Tests.Mocks;

namespace TimeToTomato.Tests
{
    [TestFixture]
    public class AssistantTests
    {
        private const int workSeconds = 60 * 25;

        Assistant _assistant;
        bool _timerActivatedOccurred;
        bool _timerStoppedOccurred;

        SecondTicker _mockTicker;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {

        }

        [SetUp]
        public void SetUp()
        {
            _mockTicker = new SecondTicker();
            MockedInfrastructureFactory mf = new MockedInfrastructureFactory();
            mf.SecondTicker = _mockTicker;
            mf.Init();

            _assistant = new Assistant();
            _timerActivatedOccurred = false;
            _timerStoppedOccurred = false;
            _assistant.TimerActivated += (o, e) => _timerActivatedOccurred = true;
            _assistant.TimerStopped += (o, e) => _timerStoppedOccurred = true;
        }

        [Test]
        public void AssistantStartWorkTimerTimerActivated()
        {
            _assistant.StartWorkTimer();

            Assert.AreEqual(workSeconds, _assistant.SecondsElapsed);
            Assert.True(_timerActivatedOccurred);
            Assert.True(_assistant.IsActive);
        }

        [Test]
        public void AssistantStopTimerTimerStopped()
        {
            _assistant.StopTimer();
            Assert.AreEqual(true, _timerStoppedOccurred);
            Assert.AreEqual(workSeconds, _assistant.SecondsElapsed);
            Assert.False(_assistant.IsActive);
        }

        [Test]
        public void AssistantTimerWorkingSecondsElapsedChange()
        {
            _assistant.StartWorkTimer();
            _mockTicker.GenerateTicks(100);

            int se = _assistant.SecondsElapsed;
            Assert.AreEqual(workSeconds - 100, se);
        }

        [Test]
        public void AssistantTimerWorkingStopingResetsSeconds()
        {
            _assistant.StartWorkTimer();
            _mockTicker.GenerateTicks(100);
            _assistant.StopTimer();

            int se = _assistant.SecondsElapsed;
            Assert.AreEqual(workSeconds, se);
        }

        [Test]
        public void AssistantTimerEventSecondsElapsed()
        {
            int eventAppeared = 0;
            _assistant.SecondsElapsedChanged += (o, e) => eventAppeared++;
            _assistant.StartWorkTimer();
            _mockTicker.GenerateTicks(100);

            Assert.AreEqual(100, eventAppeared);

            _assistant.StopTimer();

            Assert.AreEqual(101, eventAppeared);
        }

        [Test]
        public void AssistantIsActiveBeforeStart()
        {
            Assert.False(_assistant.IsActive);
        }

        [Test]
        public void AssistantIsNotActiveAfterStartAndElapsedAll()
        {
            _assistant.StartWorkTimer();
            _mockTicker.GenerateTicks(workSeconds);

            Assert.False(_assistant.IsActive);
            Assert.True(_timerActivatedOccurred);
            Assert.True(_timerStoppedOccurred);
        }
    }
}
