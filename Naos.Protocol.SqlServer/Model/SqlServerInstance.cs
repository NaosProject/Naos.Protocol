// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlServerInstance.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Protocol.SqlServer
{
    using Naos.Protocol.Domain;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.Type;

    /// <summary>
    /// SQL implementation of an <see cref="IStream" />.
    /// </summary>
    public partial class SqlServerInstance : IModelViaCodeGen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerInstance"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="instanceName">Name of the instance.</param>
        /// <param name="port">The port.</param>
        /// <param name="userName">The username.</param>
        /// <param name="password">The password.</param>
        public SqlServerInstance(
            string address,
            string instanceName,
            int port,
            string userName,
            string password)
        {
            address.MustForArg(nameof(address)).NotBeNullNorWhiteSpace();
            instanceName.MustForArg(nameof(instanceName)).NotBeNullNorWhiteSpace();
            userName.MustForArg(nameof(instanceName)).NotBeNullNorWhiteSpace();
            password.MustForArg(nameof(instanceName)).NotBeNullNorWhiteSpace();

            this.Address = address;
            this.InstanceName = instanceName;
            this.Port = port;
            this.UserName = userName;
            this.Password = password;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; private set; }

        /// <summary>
        /// Gets the name of the instance.
        /// </summary>
        /// <value>The name of the instance.</value>
        public string InstanceName { get; private set; }

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; private set; }

        /// <summary>
        /// Gets the name of the user that can create a table.
        /// </summary>
        /// <value>The name of the user that can create a table.</value>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the password for the specified user name.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; private set; }
    }
}
