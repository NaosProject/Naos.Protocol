// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiscoverLatestBackupFile.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.Domain.Test
{
    using OBeautifulCode.Validation.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Discover latest backup file.
    /// </summary>
    public class DiscoverLatestBackupFile : ReadOperationBase
    {
    }

    /// <summary>
    /// Download file.
    /// </summary>
    public class DownloadFile : ReadOperationBase
    {
    }

    /// <summary>
    /// Open gate.
    /// </summary>
    public class OpenGate : WriteOperationBase
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Prefer an interface.")]
    public interface IOpenGate : IHandleOperations<OpenGate, NoReturnType>
    {
    }

    /// <summary>
    /// Close gate.
    /// </summary>
    public class CloseGate : WriteOperationBase
    {
        public CloseGate(string gateId)
        {
            new { gateId }.Must().NotBeNullNorWhiteSpace();

            this.GateId = gateId;
        }

        public string GateId { get; private set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Prefer an interface.")]
    public interface ICloseGate : IHandleOperations<CloseGate, NoReturnType>
    {
    }

    /// <summary>
    /// Restore database.
    /// </summary>
    public class RestoreDatabase : WriteOperationBase
    {
    }

    /*
               //MemberReaderWriterRunner entityService = new ConsolidateOperationHandler<CheckoutBookFromLibrary, Book, PutBookInLibrary, NoReturnType>(handler1, handler2); // this is from magic DI land
               serivices = MagicDiFactory.Get<IGetMetricTreeHandler, ISaveTrialBalancesHandler>();
               HarnessEntry(entityService);

               // sequence
               var sequenceForReactor = new BuildSequence(
                   operations: new[]
                   {
                       _ => new DiscoverFileOperation().SaveOutput("key"),
                       _ => new DownloadFile(_.GetOutout<string>("key")),
                       _ => new CloseGate(),
                       _ => new RestoreDatabase(_.GetOutout<string>("key")),
                       _ => new OpenGate(),
                   }).WithDependency<ClockHasChanged>(OperationToEvaluate.Result<Output>().Select(_ => _.ShouldRun)));

               // at install time
               postOffice.PutSequence(new PutSequenceOperation(sequenceForReactor));
                */
}
