using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam.Dto
{
    public class FileInputDto
    {
        public long FileSize { get; set; }

        public string FileName { get; set; }

        public FileDto FileInfo { get; set; }

        public List<string> List { get; set; }
    }

    public class FileDto
    {
        public long FileSize { get; set; }

        public string FileName { get; set; }
    }
}
