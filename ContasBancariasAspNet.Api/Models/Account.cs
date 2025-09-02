using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContasBancariasAspNet.Api.Models;

[Table("tb_account")]
public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    public string Number { get; set; } = string.Empty;

    [Column(TypeName = "varchar(10)")]
    public string Agency { get; set; } = string.Empty;

    [Column(TypeName = "decimal(13,2)")]
    public decimal Balance { get; set; }

    [Column("additional_limit", TypeName = "decimal(13,2)")]
    public decimal Limit { get; set; }

    // Navigation property
    public User? User { get; set; }
}