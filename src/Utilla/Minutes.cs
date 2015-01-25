//-----------------------------------------------------------------------
// <copyright file="Minutes.cs" company="Utilla">
//     Copyright © Utilla 
// </copyright>
//-----------------------------------------------------------------------
namespace Utilla
{
    /// <summary>
    /// Contains constants related to minutes.
    /// </summary>
    public class Minutes
    {
        /// <summary>
        /// Minutes per hour.
        /// </summary>
        public const int PerHour = 60;

        /// <summary>
        /// Minutes per day.
        /// </summary>
        public const int PerDay = Minutes.PerHour * Hours.PerDay;
    }
}
