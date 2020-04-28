using System;
using System.Collections.Generic;
using Moq;

namespace TusLibros.API.UnitTests.Fakes
{
    internal class ClockStubBuilder
    {
        private readonly List<Action<Mock<IClock>>> _actions;

        internal ClockStubBuilder()
        {
            _actions = new List<Action<Mock<IClock>>>();
        }

        public ClockStubBuilder Returns(DateTime dateTime)
        {
            _actions.Add(p => p.Setup(q => q.GetDateTime())
                               .Returns(dateTime));
            return this;
        }

        public ClockStubBuilder IsExpired(bool expired)
        {
            var comparerStub = new Mock<IClockCompare>();
            comparerStub.Setup(p => p.ExpiredOn(It.IsAny<DateTime>()))
                .Returns(expired);

            _actions.Add(p => p.Setup(q => q.Has(It.IsAny<DateTime>()))
                               .Returns(comparerStub.Object));
             return this;
        }

        public IClock Build()
        {
            var clockStub = new Mock<IClock>();
            _actions.ForEach(a => a(clockStub));
            return clockStub.Object;
        }
    }
}