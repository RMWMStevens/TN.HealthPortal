namespace TN.HealthPortal.Logic.DTOs
{
    public class FileDownloadDto
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}
