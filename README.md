[![Build status](https://ci.appveyor.com/api/projects/status/556xhlr2kqc8o6s8?svg=true)](https://ci.appveyor.com/project/Naos-Project/naos-protocol)

Naos.Protocol
===============
Protocol is a paradigm/structure with associated tooling to create functional units of code.

Each task is declared as an IOperation, typically deriving from either VoidOperationBase or ReturningOperationBase<TReturn>.

The operation contains the properties required to complete the stated task.

Operations become a contract of behaviour using the operation as the declared inputs like a parameter object and the TReturn the output.

This can be thought of as a single function prototype on an interface.  However by declaring each method as an individual class you get a much richer set of options around mocking, refactoring, and declaring dependencies.

Implementation of these 'methods' is by interface implementation of IVoidProtocol<TOperation> and IReturningProtocol<TOperation, TReturn>.  There are synchronous, asynchronous, and combined interfaces. The method(s) available are only Execute(TOperation operation) and ExecuteAsync(TOperation operation).  This keeps everything focused on the public contract, the operation.

With the operations being complete model objects you get the ability to serialize the operation as well as the ease of stitching protocols together in a traditional pipes and filters pattern, similar chaining commands in the shell.

Approaching behaviour segmentation and logic articulation this way has a tendency to produce better structures all around.
