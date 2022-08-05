﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Adam.WebApi.Extensions
{
    public static class ResponseFileExtension
    {
        public static IActionResult ResponseFile(this HttpContext httpContext, string fileFullName)
        {
            string fileName = Path.GetFileName(fileFullName);
            //头部保存 文件名
            httpContext.Response.Headers.Append("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode($"{fileName}"));
            //文件类型
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            if (!System.IO.File.Exists(fileFullName))
            {
                return new EmptyResult();
            }
            string fileExtions = Path.GetExtension(fileFullName);
            //获取文件类型
            string contentType = "application/octet-stream";
            string mime = string.Empty;
            bool flag = provider.Mappings.TryGetValue(fileExtions, out mime);
            if (flag)
            {
                contentType = mime;
            }
            FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
            //读取文件流
            return new FileStreamResult(fs, contentType);
        }
    }
}