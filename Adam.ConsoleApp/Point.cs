// <copyright file="documentationcomments.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Adam.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class <c>Point</c> models a point in a two-dimensional plane.
    /// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments#d36-include.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// X.
        /// </summary>
        private int x;
        /// <summary>
        /// Y.
        /// </summary>
        private int y;

        /// <summary>
        /// This method changes the point's location by the given x- and y-offsets.
        /// <example>
        /// For example:
        /// <code>
        /// Point p = new Point(3,5);
        /// p.Translate(-1,3);
        /// </code>
        /// results in <c>p</c>'s having the value (2,8).
        /// </example>
        /// </summary>
        public void Translate(int dx, int dy)
        {
            this.x += dx;
            this.y += dy;
        }

        /// <summary>
        /// ReadRecord.
        /// <list type="bullet">
        /// <item>first
        /// <description>Item 1.</description>
        /// </item>
        /// <item>second
        /// <description>Item 2.</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="flag">flag.</param>
        /// <exception cref="MasterFileFormatCorruptException">
        /// Thrown when the master file is corrupted.
        /// </exception>
        /// <exception cref="MasterFileLockedOpenException">
        /// Thrown when the master file is already open.
        /// </exception>
        public static void ReadRecord(int flag)
        {
            Console.WriteLine($"{flag}");
        }
    }
}
