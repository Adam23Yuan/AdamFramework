namespace Adam.Dto
{
    public class FileResponseDto
    {
        public string fileType { get; set; }
        public byte[] archiveData { get; set; }
        public string archiveName { get; set; }

    }
}