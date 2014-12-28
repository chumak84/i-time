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

        [SetUp]
        public void SetUp()
        {
            _mockTicker = new SecondTicker();
            _assistant = new Assistant(_mockTicker);
            _timerActivatedOccurred = false;
            _timerStoppedOccurred = false;
            _assistant.TimerActivated += (o, e) => _timerActivatedOccurred = true;
            _assistant.TimerStopped += (o, e) => _timerStoppedOccurred = true;
        }

        [Test]
        public void AssistantStartWorkTimerTimerActivatedTest()
        {
            _assistant.StartWorkTimer();
            Assert.AreEqual(true, _timerActivatedOccurred);
        }

        [Test]
        public void AssistantStopTimerTimerStoppedTest()
        {
            _assistant.StopTimer();
            Assert.AreEqual(true, _timerStoppedOccurred);
        }

        [Test]
        public void AssistantStart25x60SecondsElapsed()
        {
            _assistant.StartWorkTimer();
            int se = _assistant.SecondsElapsed;

            Assert.AreEqual(workSeconds, se);
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
        public void AssistantTimerAutoStopElapsed()
        {
            _assistant.StartWorkTimer();
            _mockTicker.GenerateTicks(workSeconds);

            Assert.AreEqual(true, _timerStoppedOccurred);
        }

        [Test]
        public void AssistantIsActiveBeforeStart()
        {
            Assert.AreEqual(false, _assistant.IsActive);
        }
        
        [Test]
        public void AssistantIsActiveAfterStart()
        {
            _assistant.StartWorkTimer();

            Assert.AreEqual(true, _assistant.IsActive);
        }

        [Test]
        public void AssistantIsActiveAfterStartStop()
        {
            _assistant.StartWorkTimer();
            _assistant.StopTimer();

            Assert.AreEqual(false, _assistant.IsActive);
        }

        [Test]
        public void AssistantIsActiveAfterStartAndElapsedAll()
        {
            _assistant.StartWorkTimer();
            _mockTicker.GenerateTicks(workSeconds);

            Assert.AreEqual(false, _assistant.IsActive);
        }
    }
}
