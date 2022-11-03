using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Adam.Dto;
using Adam.IServices;
using Adam.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Adam.WebApi.Controllers
{
    /// <summary>
    /// V1版本API
    /// <para>https://levelup.gitconnected.com/upload-and-download-multiple-files-using-net-5-0-web-api-430f95f34237</para>
    /// <para>https://www.cnblogs.com/wangxiaorang/p/16378527.html</para>
    /// <para>https://www.cnblogs.com/JohnnyLui/p/16096643.html</para>
    /// <para>上传分：请求上传，合并，保存</para>
    /// <para>https://www.cnblogs.com/AprilBlank/p/11399509.html</para>
    /// </summary>
    [ApiController]
    [Route("api/[controller]/")]
    public class FileController : ControllerBase
    {
        #region Property   
        private IHostingEnvironment _hostingEnvironment;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor  
        public FileController(IFileService fileService, IHostingEnvironment hostingEnvironment)
        {
            _fileService = fileService;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Upload
        [HttpPost(nameof(Upload))]
        public IActionResult Upload(List<IFormFile> formFiles, [FromForm] string subDirectory)
        {
            try
            {
                IFormCollection keyValuePairs = Request.Form;
                //当 参数 formFiles 与前端传递的file控件name不一致时，获取不到上传的文件
                //可使用Request.Form.Files 获取上传的文件
                if (formFiles.Count <= 0)
                {
                    IFormFileCollection fileCollection = Request.Form.Files;
                    foreach (var item in fileCollection)
                    {
                        formFiles.Add(item);
                    }
                }
                _fileService.UploadFile(formFiles, subDirectory);

                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost(nameof(UploadBody))]//
        public IActionResult UploadBody([FromForm] List<IFormFile> formFiles, [FromForm] string subDirectory)
        {
            try
            {
                _fileService.UploadFile(formFiles, subDirectory);

                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost(nameof(UploadBodyJson))]//
        //public IActionResult UploadBodyJson([FromForm] List<IFormFile> formFiles, [FromForm] FileInputDto fileInputDto)
        //{
        //    try
        //    {
        //        _fileService.UploadFile(formFiles, nameof(UploadBodyJson));

        //        return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost(nameof(UploadBodyJsonString))]//
        public IActionResult UploadBodyJsonString([FromForm] List<IFormFile> formFiles, [FromForm] string fileInputDtoJson)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                FileInputDto fileInputDto = JsonSerializer.Deserialize<FileInputDto>(fileInputDtoJson);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                //当 参数 formFiles 与前端传递的file控件name不一致时，获取不到上传的文件
                //可使用Request.Form.Files 获取上传的文件
                if (formFiles.Count <= 0)
                {
                    IFormFileCollection fileCollection = Request.Form.Files;
                    foreach (var item in fileCollection)
                    {
                        formFiles.Add(item);
                    }
                }
                _fileService.UploadFile(formFiles, nameof(UploadBodyJsonString));

                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(UploadFormCollection))]//
        public IActionResult UploadFormCollection([FromForm] IFormCollection formCollection)
        {
            try
            {
                //获取文件上传文件的方法
                var keyValuePairs = Request.Form.Files;
                FormFileCollection fileCollection = (FormFileCollection)formCollection.Files;
                foreach (var item in fileCollection)
                {
                    //第一种
                    //StreamReader streamReader = new StreamReader(item.OpenReadStream());
                    //第二种
                    var lines1 = ReadLines(() =>
                    {
                        return item.OpenReadStream();
                    }).ToList();
                    //第三种
                    Stream memoryStream = new MemoryStream();
                    item.CopyTo(memoryStream);
                    var lines2 = ReadLines(() => memoryStream).ToList();
                }
                string JsonContent = string.Empty;
                if (formCollection.ContainsKey("JsonContent"))
                {
                    JsonContent = formCollection["JsonContent"];
                }
                string subDirectory = nameof(UploadFormCollection);
                if (formCollection.ContainsKey("subDirectory"))
                {
                    subDirectory = formCollection["subDirectory"].ToString();
                }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                FileInputDto fileInputDto = JsonSerializer.Deserialize<FileInputDto>(JsonContent);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                _fileService.UploadFile(fileCollection, subDirectory);
                return Ok(new { fileCollection.Count, Size = _fileService.SizeConverter(fileCollection.Sum(f => f.Length)) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Download File  
        [HttpGet(nameof(Download))]
        public IActionResult Download([Required] string subDirectory)
        {
            try
            {
                var fileResponseDto = _fileService.DownloadFiles(subDirectory);

                return File(fileResponseDto.archiveData, fileResponseDto.fileType, fileResponseDto.archiveName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet(nameof(DownloadBody))]
        public IActionResult DownloadBody([Required][FromBody] string subDirectory)
        {
            try
            {
                var fileResponseDto = _fileService.DownloadFiles(subDirectory);

                return File(fileResponseDto.archiveData, fileResponseDto.fileType, fileResponseDto.archiveName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 前段下载Excel模板
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(DownLoadSingleFile))]
        public IActionResult DownLoadSingleFile([FromForm] string fileName, [FromForm] string subDirectory)
        {
            //头部保存 文件名
            //HttpContext.Response.Headers.Append("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode($"{fileName}"));
            //文件类型
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            subDirectory = subDirectory ?? string.Empty;
            var targetDirectory = Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory);
            string fileFullName = targetDirectory + $"/{fileName}";
            if (!System.IO.File.Exists(fileFullName))
            {
                return new EmptyResult();
            }
            return HttpContext.ResponseFile(fileFullName);

            //string fileExtions = Path.GetExtension(fileFullName);
            ////获取文件类型
            //string mime;
            //provider.Mappings.TryGetValue(fileExtions, out mime);
            ////读取文件流
            //FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
            //return File(fs, mime, Path.GetFileName(fileFullName), true);
        }
        /// <summary>
        /// 前段下载Excel模板
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(DownLoadSingleFileStream))]
        public IActionResult DownLoadSingleFileStream([FromForm] string fileName, [FromForm] string subDirectory, [FromForm] int times)
        {
            //头部保存 文件名
            //HttpContext.Response.Headers.Append("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode($"{fileName}"));
            //文件类型
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            subDirectory = subDirectory ?? string.Empty;
            var targetDirectory = Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory);
            string fileFullName = targetDirectory + $"/{fileName}";
            if (!System.IO.File.Exists(fileFullName))
            {
                return new EmptyResult();
            }
            return HttpContext.ResponseFileStream(fileFullName, times);

            //string fileExtions = Path.GetExtension(fileFullName);
            ////获取文件类型
            //string mime;
            //provider.Mappings.TryGetValue(fileExtions, out mime);
            ////读取文件流
            //FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
            //return File(fs, mime, Path.GetFileName(fileFullName), true);
        }

        #endregion

        /// <summary>
        /// Read stream line by line.
        /// </summary>
        /// <param name="streamFactory">streamFactory.</param>
        /// <returns>Lines.</returns>
        public static IEnumerable<string> ReadLines(Func<Stream> streamFactory)
        {
            using (var stream = streamFactory())
            using (var reader = new StreamReader(stream))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
