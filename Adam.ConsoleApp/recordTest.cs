using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam.ConsoleApp
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public record class Course(string name);

    public record class Person(string name, int age);

    public record class Student(string name, int age, string className, Course Course) : Person(name, age)
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
            builder.Append("name");
            builder.Append(" : ");
            builder.Append(Name);
            builder.Append("name");
            builder.Append(" : ");
            builder.Append(Name);
            builder.Append(", ");
            builder.Append("Age");
            builder.Append(" : ");
            builder.Append(Age.ToString());
            return true;
        }
    }
}
