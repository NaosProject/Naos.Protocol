// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecificProtocolBaseTests.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using System;
    using System.Threading.Tasks;
    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    public static partial class SerializationConfigurationTypes
    {
        [Fact]
        public static async Task SyncOnlyVoidProtocol_ExecuteAsync()
        {
            // Arrange
            var operation = new MyVoidOperation();
            var protocol = new SyncOnlyVoidProtocol();

            // Act
            await protocol.ExecuteAsync(operation);

            // Assert
            operation.OperationExecuted.MustForTest().BeTrue();
        }

        [Fact]
        public static void SyncOnlyVoidProtocol_Execute()
        {
            // Arrange
            var operation = new MyVoidOperation();
            var protocol = new SyncOnlyVoidProtocol();

            // Act
            protocol.Execute(operation);

            // Assert
            operation.OperationExecuted.MustForTest().BeTrue();
        }

        [Fact]
        public static async Task AsyncOnlyVoidProtocol_ExecuteAsync()
        {
            // Arrange
            var operation = new MyVoidOperation();
            var protocol = new AsyncOnlyVoidProtocol();

            // Act
            await protocol.ExecuteAsync(operation);

            // Assert
            operation.OperationExecuted.MustForTest().BeTrue();
        }

        [Fact]
        public static void AsyncOnlyVoidProtocol_Execute()
        {
            // Arrange
            var operation = new MyVoidOperation();
            var protocol = new AsyncOnlyVoidProtocol();

            // Act
            protocol.Execute(operation);

            // Assert
            operation.OperationExecuted.MustForTest().BeTrue();
        }
        
        [Fact]
        public static async Task SyncOnlyReturningProtocol_ExecuteAsync()
        {
            // Arrange
            var operation = new MyReturningOperation();
            var protocol = new SyncOnlyReturningProtocol();

            // Act
            var result = await protocol.ExecuteAsync(operation);

            // Assert
            operation.OperationExecuted.MustForTest().BeTrue();
            result.MustForTest().BeEmptyString();
        }

        [Fact]
        public static void SyncOnlyReturningProtocol_Execute()
        {
            // Arrange
            var operation = new MyReturningOperation();
            var protocol = new SyncOnlyReturningProtocol();

            // Act
            var result = protocol.Execute(operation);

            // Assert
            operation.OperationExecuted.MustForTest().BeTrue();
            result.MustForTest().BeEmptyString();
        }

        [Fact]
        public static async Task AsyncOnlyReturningProtocol_ExecuteAsync()
        {
            // Arrange
            var operation = new MyReturningOperation();
            var protocol = new AsyncOnlyReturningProtocol();

            // Act
            var result = await protocol.ExecuteAsync(operation);

            // Assert
            operation.OperationExecuted.MustForTest().BeTrue();
            result.MustForTest().BeEmptyString();
        }

        [Fact]
        public static void AsyncOnlyReturningProtocol_Execute()
        {
            // Arrange
            var operation = new MyReturningOperation();
            var protocol = new AsyncOnlyReturningProtocol();

            // Act
            var result = protocol.Execute(operation);

            // Assert
            operation.OperationExecuted.MustForTest().BeTrue();
            result.MustForTest().BeEmptyString();
        }

        private class SyncOnlyVoidProtocol : SyncSpecificVoidProtocolBase<MyVoidOperation>
        {
            /// <inheritdoc />
            public override void Execute(
                MyVoidOperation operation)
            {
                operation.MustForArg(nameof(operation)).NotBeNull();
                operation.OperationExecuted = true;
            }
        }

        private class AsyncOnlyVoidProtocol : AsyncSpecificVoidProtocolBase<MyVoidOperation>
        {
            /// <inheritdoc />
            public override async Task ExecuteAsync(
                MyVoidOperation operation)
            {
                operation.MustForArg(nameof(operation)).NotBeNull();
                var setValue = await Task.FromResult(true);
                operation.OperationExecuted = setValue;
            }
        }

        private class MyVoidOperation : VoidOperationBase
        {
            public MyVoidOperation()
            {
                this.OperationExecuted = false;
            }

            public bool OperationExecuted { get; set; }
        }

        private class SyncOnlyReturningProtocol : SyncSpecificReturningProtocolBase<MyReturningOperation, string>
        {
            /// <inheritdoc />
            public override string Execute(
                MyReturningOperation operation)
            {
                operation.MustForArg(nameof(operation)).NotBeNull();
                operation.OperationExecuted = true;
                return string.Empty;
            }
        }

        private class AsyncOnlyReturningProtocol : AsyncSpecificReturningProtocolBase<MyReturningOperation, string>
        {
            /// <inheritdoc />
            public override async Task<string> ExecuteAsync(
                MyReturningOperation operation)
            {
                operation.MustForArg(nameof(operation)).NotBeNull();
                var setValue = await Task.FromResult(true);
                operation.OperationExecuted = setValue;
                return string.Empty;
            }
        }

        private class MyReturningOperation : ReturningOperationBase<string>
        {
            public MyReturningOperation()
            {
                this.OperationExecuted = false;
            }

            public bool OperationExecuted { get; set; }
        }
    }
}
