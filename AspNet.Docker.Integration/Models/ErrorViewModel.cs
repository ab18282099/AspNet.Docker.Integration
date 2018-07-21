namespace AspNet.Docker.Integration.Models
{
    /// <summary>
    /// 錯誤資訊模型
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// HTTP Request 編號
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 是否顯示 Request 編號
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}