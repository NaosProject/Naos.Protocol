// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrudOperationHandlers.Get.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using Naos.Protocol.Domain;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// TODO: Starting point for new project.
    /// </summary>
    /// <typeparam name="TObject">The type of payload.</typeparam>
    public partial class CrudOperationHandlers<TObject>
#pragma warning restore CS1710 // XML comment has a duplicate typeparam tag
        where TObject : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrudOperationHandlers{TObject}"/> class.
        /// </summary>
        public CrudOperationHandlers()
        {

        }
    }
}
