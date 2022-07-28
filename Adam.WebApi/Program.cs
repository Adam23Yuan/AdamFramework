using Adam.WebApi.Utility;
using Microsoft.OpenApi.Models;
using System.Reflection;

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
// git rebase cmd -main update
builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
