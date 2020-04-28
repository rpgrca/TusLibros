using System;
using System.Collections.Generic;
using Moq;

namespace TusLibros.API.UnitTests.Fakes
{
    internal class AuthenticatorStubBuilder
    {
        private readonly List<Action<Mock<IAuthenticator>>> _actions;

        internal AuthenticatorStubBuilder()
        {
            _actions = new List<Action<Mock<IAuthenticator>>>();
        }

        public AuthenticatorStubBuilder Returns(bool loginResult)
        {
            _actions.Add((p) => p.Setup(q => q.Login(It.IsAny<string>(), It.IsAny<string>()))
                                 .Returns(loginResult));
            return this;
        }

        public IAuthenticator Build()
        {
            var authenticatorStub = new Mock<IAuthenticator>();
            _actions.ForEach(a => a(authenticatorStub));
            return authenticatorStub.Object;
        }
    }
}