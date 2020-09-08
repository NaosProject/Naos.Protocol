// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAllResourceLocatorsOp.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain
{
    using System;
    using System.Collections.Generic;
    using Naos.Protocol.Domain.Internal;

    /// <summary>
    /// Operation to a <see cref="ResourceLocatorBase"/> by a <see cref="Type"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AllStream", Justification = NaosSuppressBecause.CA1702_CompoundWordsShouldBeCasedCorrectly_AnalyzerIsIncorrectlyDetectingCompoundWordsInUnitTestMethodName)]
    public partial class GetAllResourceLocatorsOp : ReturningOperationBase<IReadOnlyCollection<ResourceLocatorBase>>
    {
    }
}
