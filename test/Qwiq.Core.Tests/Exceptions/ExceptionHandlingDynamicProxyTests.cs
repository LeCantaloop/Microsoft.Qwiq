using System;
using System.Linq;
using Microsoft.Qwiq.Core.Tests.Mocks;
using Microsoft.Qwiq.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Qwiq.Exceptions.Tests
{
    public class ExceptionHandlingDynamicProxyFactoryContextSpecification : ContextSpecification
    {
        protected IExceptionThrower InstanceToProxy { get; set; }

        protected IExceptionThrower ProxiedInstance { get; set; }

        public override void Given()
        {
            ProxiedInstance =
                ExceptionHandlingDynamicProxyFactory.Create(
                        InstanceToProxy,
                            Enumerable.Empty<IExceptionExploder>(),
                            new[]
                            {
                                new MockArgumentExceptionMapper()
                            });
        }
    }

    [TestClass]
    public class given_a_exceptionhandling_proxied_class_when_an_understood_exception_is_thrown_by_the_proxied_class : ExceptionHandlingDynamicProxyFactoryContextSpecification
    {
        public override void Given()
        {
            InstanceToProxy = new ExceptionThrower(MockArgumentExceptionMapper.MockParamName);
            base.Given();
        }

        [TestMethod]
        [ExpectedException(typeof(MockException))]
        public void the_expected_exception_type_is_rethrown()
        {
            ProxiedInstance.ThrowException();
        }
    }

    [TestClass]
    public class given_a_exceptionhandling_proxied_class_when_an_unknown_exception_is_thrown_by_the_proxied_class : ExceptionHandlingDynamicProxyFactoryContextSpecification
    {
        public override void Given()
        {
            InstanceToProxy = new ExceptionThrower("unknown");
            base.Given();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void the_thrown_exception_is_untouched()
        {
            ProxiedInstance.ThrowException();
        }
    }
}
