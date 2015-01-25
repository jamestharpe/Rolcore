//-----------------------------------------------------------------------
// <copyright file="TcpIpUtils.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla.Net.Sockets
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Utility methods for working with TCP/IP.
    /// </summary>
    public static class TcpIpUtils
    {
        /// <summary>
        /// Converts an IP address to a <see cref="double"/> representation.
        /// </summary>
        /// <param name="ipAddress">Specifies the IP address (in 0.0.0.0 format) to convert.</param>
        /// <returns>A <see cref="double"/> representation of the specified IP address.</returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        public static double IpStringToDouble(string ipAddress)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(ipAddress), "ipAddress is null or empty");

            double result = 0;
            string[] ipAddressParts = ipAddress.Split('.');
            if (ipAddressParts.Length > 1)
            {
                for (int i = ipAddressParts.Length - 1; i >= 0; i--)
                {
                    var octet = int.Parse(ipAddressParts[i]);
                    var mod = octet % 256;
                    result += mod * System.Math.Pow(256, 3 - i);
                }
            }

            return result;
        }
    }
}
