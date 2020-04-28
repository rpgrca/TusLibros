using System;
using System.Collections.Generic;
using Moq;

namespace TusLibros.API.UnitTests.Fakes
{
    public class MerchantAdapterStubBuilder
    {
        public class MerchantAdapterStubPayerResolver
        {
            private readonly decimal _value;
            private readonly CreditCard _creditCard;
            private readonly MerchantAdapterStubBuilder _builder;

            internal MerchantAdapterStubPayerResolver(decimal value, CreditCard creditCard, MerchantAdapterStubBuilder builder)
            {
                _value = value;
                _creditCard = creditCard;
                _builder = builder;
            }

            public MerchantAdapterStubBuilder Throw(Exception exception)
            {
                _builder._actions.Add(a => a.Setup(p => p.Debit(_value, _creditCard))
                                            .Throws(exception));
                return _builder;
            }

            public MerchantAdapterStubBuilder Return(string returnValue)
            {
                _builder._actions.Add(a => a.Setup(p => p.Debit(_value, _creditCard))
                                            .Returns(returnValue));
                return _builder;
            }
        }

        public class MerchantAdapterStubPayerSetup
        {
            private readonly MerchantAdapterStubBuilder _builder;
            private readonly CreditCard _creditCard;

            internal MerchantAdapterStubPayerSetup(CreditCard creditCard, MerchantAdapterStubBuilder builder)
            {
                _creditCard = creditCard;
                _builder = builder;
            }

            public MerchantAdapterStubPayerResolver ToPay(decimal value)
            {
                return new MerchantAdapterStubPayerResolver(value, _creditCard, _builder);
            }

            public MerchantAdapterStubBuilder Throw(Exception exception)
            {
                _builder._actions.Add(a => a.Setup(p => p.Debit(It.IsAny<decimal>(), _creditCard))
                                            .Throws(exception));
                return _builder;
            }

            public MerchantAdapterStubBuilder Return(string resultValue)
            {
                _builder._actions.Add(a => a.Setup(p => p.Debit(It.IsAny<decimal>(), _creditCard))
                                            .Returns(resultValue));
                return _builder;
            }
        }

        private readonly List<Action<Mock<IMerchantAdapter>>> _actions = new List<Action<Mock<IMerchantAdapter>>>();

        public MerchantAdapterStubBuilder AlwaysThrows(Exception exception)
        {
            _actions.Add(a => a.Setup(p => p.Debit(It.IsAny<decimal>(), It.IsAny<CreditCard>()))
                               .Throws(exception));
            return this;
        }

        public MerchantAdapterStubPayerSetup WhenUsing(CreditCard creditCard)
        {
            return new MerchantAdapterStubPayerSetup(creditCard, this);
        }

        public IMerchantAdapter Build()
        {
            var merchantStub = new Mock<IMerchantAdapter>();
            _actions.ForEach(a => a(merchantStub));

            return merchantStub.Object;
        }
    }
}