﻿// <copyright file="recordTest.cs" company="Microsoft">
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
    ///
    /// </summary>
    /// <param name="name"></param>
    public record class Course(string name);

    public record class Person(string name, int age);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:Closing parenthesis should be spaced correctly", Justification = "<Pending>")]
    public record class Student(string name, int age, string className, Course course) : Person(name, age)
    {

    }

    public record struct goods(string name, int price);

    public readonly record struct goods2(string name, int price);

    public record class CustomerRecord(string name)
    {
        public string Name { get; set; }
        public string Age { get; set; }

        public void showMesage()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Console.WriteLine(this.PrintMembers(stringBuilder));
            Console.WriteLine(stringBuilder.ToString());
        }

        protected virtual bool PrintMembers(StringBuilder builder)
        {
            if (builder == null)
            {
                return true;
            }

            builder.Append("name");
            builder.Append(" : ");
            builder.Append(this.Name);
            builder.Append("name");
            builder.Append(" : ");
            builder.Append(this.Name);
            builder.Append(", ");
            builder.Append("Age");
            builder.Append(" : ");
            builder.Append(this.Age.ToString());
            return true;
        }
    }
}
