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
        [Test]
        public void AssistantRunWorkTimerTest()
        {
            Assistant a = new Assistant();
            a.StartWorkTimer();
        }
    }
}
