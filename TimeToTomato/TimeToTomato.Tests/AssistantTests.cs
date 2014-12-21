using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeToTomato.Model;

namespace TimeToTomato.Tests
{
    [TestFixture]
    public class AssistantTests
    {
        Assistant _assistant;
        bool _timerActivatedOccurred;
        bool _timerStoppedOccurred;

        [SetUp]
        public void SetUp()
        {
            _assistant = new Assistant();
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
    }
}
