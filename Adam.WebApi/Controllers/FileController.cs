using Adam.Dto;
using Adam.IServices;
using Adam.WebApi.Utility;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Adam.WebApi.Controllers
{
    /// <summary>
    /// V1版本API
    /// <para>https://levelup.gitconnected.com/upload-and-download-multiple-files-using-net-5-0-web-api-430f95f34237</para>
    /// </summary>
    [ApiController]
    [Route("api/[controller]/")]
    public class FileController : ControllerBase
    {
        #region Property  
        private readonly IFileService _fileService;
        #endregion

        #region Constructor  
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        #endregion

        #region Upload
        [HttpPost(nameof(Upload))]
        public IActionResult Upload(List<IFormFile> formFiles, string subDirectory)
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
        [HttpPost(nameof(UploadBodyJson))]//
        public IActionResult UploadBodyJson([FromForm] List<IFormFile> formFiles, [FromForm] FileInputDto fileInputDto)
        {
            try
            {
                _fileService.UploadFile(formFiles, nameof(UploadBodyJson));

                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost(nameof(UploadBodyJsonString))]//
        public IActionResult UploadBodyJsonString([FromForm] List<IFormFile> formFiles, [FromForm] string fileInputDtoJson)
        {
            try
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                FileInputDto fileInputDto = JsonSerializer.Deserialize<FileInputDto>(fileInputDtoJson);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                FileInputDto fileInputDto = JsonSerializer.Deserialize<FileInputDto>(JsonContent);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                _fileService.UploadFile(fileCollection, nameof(UploadFormCollection));

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
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}