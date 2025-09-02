using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContasBancariasAspNet.Api.Models;

[Table("tb_card")]
public class Card
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(16)")]
    public string Number { get; set; } = string.Empty;

    [Column("available_limit", TypeName = "decimal(13,2)")]
    public decimal Limit { get; set; }

    // Navigation property
    public User? User { get; set; }
}