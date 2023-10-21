namespace FileService.Application.DTOs.MediaFiles
{
    public class DownloadDTO
    {
        #region Constructors
        public DownloadDTO(Stream stream, string mimeType)
        {
            Stream = stream;
            MimeType = mimeType;
        }
        #endregion


        #region Properties
        /// <summary>
        /// استریم فایل
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// نوع محتوای فایل
        /// مثلا application/octet-stream
        /// </summary>
        public string MimeType { get; set; } 
        #endregion
    }
}
