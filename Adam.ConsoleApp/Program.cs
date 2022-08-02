// See https://aka.ms/new-console-template for more information
using Adam.Dto;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");
string strL = "hello";
Console.WriteLine(nameof(strL));//CC
Console.WriteLine(strL);//CC
Console.WriteLine(nameof(System.ConsoleColor));//ConsoleColor
Console.WriteLine(typeof(System.ConsoleColor).FullName);//ConsoleColor

FileDto file = new FileDto();
file.FileSize = 20;
file.FileName = nameof(FileDto);

FileInputDto inputFile = new FileInputDto();    
inputFile.FileSize = 20;
inputFile.FileName = nameof(FileInputDto);
inputFile.FileInfo = file;
inputFile.List = new List<string> { nameof(FileDto),nameof(FileInputDto),"Adam.ConsoleApp"};

Console.WriteLine(JsonSerializer.Serialize(inputFile));

Console.ReadKey();