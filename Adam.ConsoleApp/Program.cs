// See https://aka.ms/new-console-template for more information
using Adam.ConsoleApp;

Console.WriteLine("Hello, World!");

#region nameof
{
    //string strL = "hello";
    //Console.WriteLine(nameof(strL));//CC
    //Console.WriteLine(strL);//CC
    //Console.WriteLine(nameof(System.ConsoleColor));//ConsoleColor
    //Console.WriteLine(typeof(System.ConsoleColor).FullName);//ConsoleColor

    //FileDto file = new FileDto();
    //file.FileSize = 20;
    //file.FileName = nameof(FileDto);

    //FileInputDto inputFile = new FileInputDto();
    //inputFile.FileSize = 20;
    //inputFile.FileName = nameof(FileInputDto);
    //inputFile.FileInfo = file;
    //inputFile.List = new List<string> { nameof(FileDto), nameof(FileInputDto), "Adam.ConsoleApp" };

    //Console.WriteLine(JsonSerializer.Serialize(inputFile));
}
#endregion

#region record(record class,record struct,readonly record struct)
{
    //recordTest.Test1();
}
#endregion

#region Nullable<T>
{
    double? currentvalue = null;
    if (!currentvalue.HasValue)
    {
        Console.WriteLine($"currentvalue is null");
        currentvalue = 0.2384;
    }
    if (currentvalue.HasValue)
    {
        Console.WriteLine($"currentvalue is {currentvalue.Value}");
    }
}
#endregion

Console.ReadKey();


public class recordTest
{
    public static void Test1()
    {
        var course = new Course("english");
        var person = new Person("adam", 23);
        //person.name = "";
        var person2 = person with { age = 23 };
        var person3 = new Person("adam", 23);
        var student = new Student("adan.student", 25, "english", course);
        var student2 = new Student("adan.student", 25, "english", course);


        Console.WriteLine($"person== person2=>{person == person2}");
        Console.WriteLine($"person== person3=>{person == person3}");
        Console.WriteLine($"person2== person3=>{person2 == person3}");
        Console.WriteLine($"ReferenceEquals(person, person2)=>{ReferenceEquals(person, person2)}");
        Console.WriteLine($"ReferenceEquals(person, person3)=>{ReferenceEquals(person, person3)}");
        Console.WriteLine($"ReferenceEquals(person2, person3)=>{ReferenceEquals(person2, person3)}");


        Console.WriteLine($"student== student2=>{student == student2}");
        Console.WriteLine($"ReferenceEquals(student, student2)=>{ReferenceEquals(student, student2)}");


        Console.WriteLine(student2.ToString());
        Console.WriteLine(student2);

        var goods = new goods("c# class", 99);
        goods.name = "css class";
        var goods2 = new goods2("c# class", 99);

        var goods3 = new goods2();
        //goods2.name = "css class";

        CustomerRecord customerRecord = new CustomerRecord("adam");
        //构造函数属性不可修改
        //customerRecord.name = "adam.construct";
        //自定义属性 可修改
        customerRecord.Name = "adam.rename";
        customerRecord.Age = "adam.age";

        Console.WriteLine(customerRecord.ToString());
        customerRecord.showMesage();
    }
}
