using System.Reflection;
using Adam.IServices;
using Adam.Services;
using Adam.WebApi.Options;
using Adam.WebApi.Utility;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 改点内容吧
// master branch 添加内容
// 改点内容吧-devmaster branch
// gitdev/remote 修改内容
// gitdev/remote 第二次修改内容
// gitdev/remote 第三次修改内容
// gitdev/remote 第四次修改内容
// test git diff cmd
// test git diff cmd not commmit
// test git commit edit pattern
// git commit edit pattern must english input  
// git rebase cmd gitrebase branch commit 
// git rebase cmd gitrebase branch commit again 
// git rebase cmd -main update 
builder.Services.AddControllers();
// appsetting.json
builder.Services.Configure<PositionOptions>(builder.Configuration.GetSection(PositionOptions.Position));
builder.Services.Configure<TopItemSettings>(TopItemSettings.Month,
    builder.Configuration.GetSection("TopItem:Month"));
builder.Services.Configure<TopItemSettings>(TopItemSettings.Year,
    builder.Configuration.GetSection("TopItem:Year"));
// 验证只使用一种方案即可，两种方案会重复执行。

// 验证一：注册+验证分开注册
builder.Services.Configure<MyConfigOptions>(builder.Configuration.GetSection(MyConfigOptions.MyConfig));
builder.Services.AddSingleton<IValidateOptions<MyConfigOptions>, MyConfigValidation>();
builder.Services.PostConfigure<PositionOptions>(myOptions =>
{
    myOptions.Name += "PostConfigureAll";
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


//builder.Services.AddOptions<TopItemSettings>().Configure<>
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
