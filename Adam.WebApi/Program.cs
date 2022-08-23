using System.Reflection;
using Adam.IServices;
using Adam.Services;
using Adam.WebApi.Options;
using Adam.WebApi.Utility;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// 自定义扩展 配置源
builder.Configuration.AddJsonFile($"config/json.json");
builder.Configuration.AddIniFile($"config/ini.ini");
builder.Configuration.AddXmlFile($"config/xml.xml");
// 命令行参数
//builder.Configuration.AddCommandLine();
// 环境变量
builder.Configuration.AddEnvironmentVariables("Common");

// Add services to the container. 
builder.Services.AddControllers();

builder.Services.AddDataProtection();
// appsetting.json
builder.Services.Configure<PositionOptions>(builder.Configuration.GetSection(PositionOptions.Position));
builder.Services.Configure<TopItemSettings>(TopItemSettings.Month,
    builder.Configuration.GetSection("TopItem:Month"));
builder.Services.Configure<TopItemSettings>(TopItemSettings.Year,
    builder.Configuration.GetSection("TopItem:Year"));
// 不同的类库实现
////builder.Services.Configure<MyConfigOptions>("Var1", options =>
////{
////    options.Key2 = 1;
////});
///
// 验证只使用一种方案即可，两种方案会重复执行。
// 验证一：注册+验证分开注册
builder.Services.Configure<MyConfigOptions>(builder.Configuration.GetSection(MyConfigOptions.MyConfig));
builder.Services.AddSingleton<IValidateOptions<MyConfigOptions>, MyConfigValidation>();
builder.Services.PostConfigure<PositionOptions>(myOptions =>
{
    myOptions.Name += "PostConfigureAll";
});
builder.Services.PostConfigure<PositionOptions>(myOptions =>
{
    myOptions.Name += "——again";
});
//// 验证二：IOptions 验证  注册+验证
//builder.Services.AddOptions<MyConfigOptions>()
//            .Bind(builder.Configuration.GetSection(MyConfigOptions.MyConfig))
//            // 注解特性验证
//            .ValidateDataAnnotations()
//            // 扩展验证
//            .Validate(config =>
//            {
//                if (config.Key2 != 0)
//                {
//                    return config.Key3 > config.Key2;
//                }
//                return true;
//            }, "error message");

// 从DI中继续获取已配置的options 继续 配置options
builder.Services.AddOptions<PositionOptions>().Configure<IOptionsFactory<TopItemSettings>, IOptionsFactory<FormOptions>>((options, factoryPositionOptions, factoryFormOptions) =>
{
    options.Name += $"【AddOptions<TopItemSettings>().Configure<IOptionsFactory<PositionOptions>, IOptionsFactory<FormOptions>>】";
});
// register form limit size
builder.Services.Configure<FormOptions>(options =>
{
    options.BufferBodyLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
    options.ValueLengthLimit = int.MaxValue;
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
// register services 
builder.Services.AddTransient<IFileService, FileService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //swagger多版本
    foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
    {
        c.SwaggerDoc(field.Name, new OpenApiInfo()
        {
            Title = $"您选择的是[Title={field.Name}]版本",
            Version = $"[Version={field.Name}]",
            Description = $"Description=[Adam.WebApi({field.Name})版本]",
        });
    }
    //swagger接口注释
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    string basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
    string xmlPath = Path.Combine(basePath, "swagger.xml");
#pragma warning restore CS8604 // Possible null reference argument.
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();
// 输出配置项
var collections = builder.Configuration.AsEnumerable();
foreach (var item in collections)
{
    Console.WriteLine("{0}={1}", item.Key, item.Value);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
        {
            c.SwaggerEndpoint($"/swagger/{field.Name}/swagger.json", $"{field.Name}");
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
