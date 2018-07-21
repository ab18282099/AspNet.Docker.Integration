namespace AspNet.Docker.Integration.Models
{
    /// <summary>
    /// ���~��T�ҫ�
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// HTTP Request �s��
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// �O�_��� Request �s��
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}