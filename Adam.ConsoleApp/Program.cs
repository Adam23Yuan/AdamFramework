// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
string strL = "hello";
Console.WriteLine(nameof(strL));//CC
Console.WriteLine(strL);//CC
Console.WriteLine(nameof(System.ConsoleColor));//ConsoleColor
Console.WriteLine(typeof(System.ConsoleColor).FullName);//ConsoleColor

Console.ReadKey();