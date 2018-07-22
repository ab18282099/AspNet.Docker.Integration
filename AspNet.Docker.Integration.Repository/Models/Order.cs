using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet.Docker.Integration.Repository.Models
{
    /// <summary>
    /// 訂單資料
    /// </summary>
    [Table("ORDER")]
    public class Order
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        [Column("ORDER_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// 使用者編號
        /// </summary>
        [Column("ORDER_USERID")]
        public int UserId { get; set; }
    }
}