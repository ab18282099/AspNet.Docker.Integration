using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNet.Docker.Integration.Repository.Models
{
    /// <summary>
    /// 使用者資料表
    /// </summary>
    [Table("user")]
    public class User
    {
        /// <summary>
        /// 使用者編號
        /// </summary>
        [Column("user_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Column("user_name")]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}