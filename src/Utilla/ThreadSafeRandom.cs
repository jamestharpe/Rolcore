//-----------------------------------------------------------------------
// <copyright file="ThreadSafeRandom.cs" company="Utilla">
//     Copyright © Utilla Contributors
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Provides a thread-safe, statistically random operations. Use instead of 
    /// <see cref="Random"/>.
    /// </summary>
    public static class ThreadSafeRandom
    {
        /// <summary>
        /// Provides random seed for thread-safe <see cref="Random"/> instances.
        /// </summary>
        private static readonly Random Global = new Random();

        /// <summary>
        /// The thread-local <see cref="Random"/> instance used to perform random functions.
        /// </summary>
        [ThreadStatic]
        private static Random local;

        /// <summary>
        /// Returns a non-negative random number.
        /// </summary>
        /// <returns>A 32-bit signed integer greater than or equal to zero.</returns>
        public static int Next()
        {
            EnsureLocal();
            return local.Next();
        }

        /// <summary>
        /// Returns a non-negative random number less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. 
        /// Must be greater than or equal to zero.</param>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than 
        /// maxValue.</returns>
        public static int Next(int maxValue)
        {
            Contract.Requires<ArgumentOutOfRangeException>(maxValue >= 0, "maxValue is less than zero");

            EnsureLocal();
            return local.Next(maxValue);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. 
        /// Must be greater than or equal to zero.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue.</returns>
        public static int Next(int minValue, int maxValue)
        {
            Contract.Requires<ArgumentOutOfRangeException>(minValue <= maxValue, "minValue is greater than maxValue");
            EnsureLocal();

            return local.Next(minValue, maxValue);
        }

        /// <summary>
        /// Ensures a thread-local <see cref="Random"/> instance is available.
        /// </summary>
        private static void EnsureLocal()
        {
            if (local == null)
            {
                int seed;
                lock (Global)
                {
                    seed = Global.Next();
                }

                local = new Random(seed);
            }
        }
    }
}
