// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolFactory.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OBeautifulCode.Representation.System;
    using OBeautifulCode.Type.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Fully open interface.
    /// </summary>
    public class ProtocolFactory : IProtocolFactory
    {
        private readonly IReadOnlyDictionary<Type, Func<IProtocol>> configuredProtocols;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolFactory"/> class.
        /// </summary>
        /// <param name="configuredProtocols">The configured protocols.</param>
        public ProtocolFactory(
            IReadOnlyDictionary<Type, Func<IProtocol>> configuredProtocols)
        {
            this.configuredProtocols = configuredProtocols ?? throw new ArgumentNullException(nameof(configuredProtocols));
        }

        /// <inheritdoc />
        public IProtocol Execute(
            GetProtocolByTypeOp operation)
        {
            var protocolType = operation.ProtocolType.ResolveFromLoadedTypes();
            var found = this.configuredProtocols.TryGetValue(protocolType, out var resultFunc);

            if (!found)
            {
                switch (operation.MissingProtocolStrategy)
                {
                    case MissingProtocolStrategy.Throw:
                        throw new ArgumentException(
                            Invariant(
                                $"Could not find a configured protocol for the specified object type: {protocolType.ToStringReadable()}."));
                    case MissingProtocolStrategy.Null:
                        return null;
                    default:
                        throw new NotSupportedException(Invariant($"The {nameof(MissingProtocolStrategy)} '{operation.MissingProtocolStrategy}' is not supported."));
                }
            }
            else
            {
                if (resultFunc == null)
                {
                    throw new ArgumentNullException(nameof(this.configuredProtocols), Invariant($"The {nameof(this.configuredProtocols)} had a null entry for the type '{protocolType.ToStringReadable()}'."));
                }

                var result = resultFunc();
                if (result == null)
                {
                    throw new InvalidOperationException(Invariant($"The configured function returned a null result."));
                }

                var actualType = result.GetType();
                if (!actualType.IsAssignableTo(protocolType))
                {
                    throw new ArgumentException(
                        Invariant(
                            $"Could not find a configured protocol assignable to the expected type '{protocolType.ToStringReadable()}', found a protocol registered for this type as '{actualType.ToStringReadable()}'."));
                }

                return result;
            }
        }

        /// <inheritdoc />
        public async Task<IProtocol> ExecuteAsync(
            GetProtocolByTypeOp operation)
        {
            var syncResult = this.Execute(operation);
            var result = await Task.FromResult(syncResult);
            return result;
        }
    }
}
