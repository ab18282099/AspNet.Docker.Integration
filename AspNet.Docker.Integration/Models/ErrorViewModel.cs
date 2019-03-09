namespace AspNet.Docker.Integration.Models
{
    /// <summary>
    /// Error data model
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// HTTP Request id
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Show request id if not null or empty
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}