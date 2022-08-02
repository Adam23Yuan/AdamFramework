using Adam.IServices;
using Adam.WebApi.Utility;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        #region Uploads  
        [HttpPost(nameof(Upload))]
        public IActionResult Upload([Required] List<IFormFile> formFiles, string subDirectory)
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
        #endregion
    }
}