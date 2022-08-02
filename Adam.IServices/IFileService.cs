using Adam.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam.IServices
{
    public interface IFileService
    {
        void UploadFile(List<IFormFile> files, string subDirectory);
        FileResponseDto DownloadFiles(string subDirectory);

        string SizeConverter(long bytes);
    }
}
