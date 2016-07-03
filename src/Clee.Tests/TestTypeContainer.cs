﻿using System;
using System.Collections;
using Moq;
using Xunit;

namespace Clee.Tests
{
    public class TestTypeContainer
    {
        [Fact]
        public void can_resolve_simple_type_by_generic_argument()
        {
            var sut = new TypeContainerBuilder().Build();
            var result = sut.Resolve<NoArgumentConstructorType>();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<NoArgumentConstructorType>(result);
        }

        [Fact]
        public void can_resolve_simple_type_by_type_argument()
        {
            var sut = new TypeContainerBuilder().Build();
            var result = sut.Resolve(typeof(NoArgumentConstructorType));

            Assert.NotNull(result);
            Assert.IsAssignableFrom<NoArgumentConstructorType>(result);
        }

        [Fact]
        public void can_resolve_semi_complex_type_with_constructor_arguments()
        {
            var sut = new TypeContainerBuilder().Build();
            var result = sut.Resolve<SingleArgumentConstructorType>();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<SingleArgumentConstructorType>(result);
        }

        [Fact]
        public void can_resolve_complex_type_with_multiple_constructor_arguments()
        {
            var sut = new TypeContainerBuilder().Build();
            var result = sut.Resolve<MultipleArgumentConstructorType>();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<MultipleArgumentConstructorType>(result);
        }

        [Fact]
        public void can_resolve_complex_type_with_multiple_nested_constructor_arguments()
        {
            var sut = new TypeContainerBuilder().Build();
            var result = sut.Resolve<NestedMultipleArgumentConstructorType>();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<NestedMultipleArgumentConstructorType>(result);
        }

        [Fact]
        public void disposable_instances_are_disposed_when_released()
        {
            var mock = new Mock<IDisposable>();
            var sut = new TypeContainerBuilder().Build();
            
            sut.Release(mock.Object);

            mock.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public void disposable_dependencies_are_disposed_when_root_is_released()
        {
            var dep1 = new DisposableType();
            var dep2 = new DisposableType();
            var owner = new DisposableOwner(dep1, dep2);

            var list = new Queue();
            list.Enqueue(dep1);
            list.Enqueue(dep2);
            list.Enqueue(owner);

            var creator = new HumbleCreator((type, dependencies) =>
            {
                return list.Dequeue();
            });

            var sut = new TypeContainerBuilder()
                .WithCreator(creator)
                .Build();

            var result = sut.Resolve<DisposableOwner>();

            sut.Release(result);

            Assert.True(owner.wasDisposed);
            Assert.True(dep1.wasDisposed);
            Assert.True(dep2.wasDisposed);
        }

        #region dummy types

        private class DisposableType : IDisposable
        {
            public bool wasDisposed = false;
            
            public void Dispose()
            {
                wasDisposed = true;
            }
        }

        private class DisposableOwner : DisposableType
        {
            public DisposableOwner(DisposableType arg1, DisposableType arg2)
            {
                Arg1 = arg1;
                Arg2 = arg2;
            }

            public DisposableType Arg1 { get; private set; }
            public DisposableType Arg2 { get; private set; }
        }

        private class NoArgumentConstructorType
        {
            
        }

        private class SingleArgumentConstructorType
        {
            public SingleArgumentConstructorType(NoArgumentConstructorType arg1)
            {
                
            }
        }

        private class MultipleArgumentConstructorType
        {
            public MultipleArgumentConstructorType(NoArgumentConstructorType arg1, NoArgumentConstructorType arg2)
            {
                
            }
        }

        private class NestedMultipleArgumentConstructorType
        {
            public NestedMultipleArgumentConstructorType(NoArgumentConstructorType arg1, SingleArgumentConstructorType arg2, MultipleArgumentConstructorType arg3)
            {
                
            }
        }

        #endregion
    }
}