using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContasBancariasAspNet.Api.Models;

[Table("tb_user")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;

    // Foreign Keys
    public long? AccountId { get; set; }
    public long? CardId { get; set; }

    // Navigation Properties
    [ForeignKey("AccountId")]
    public Account? Account { get; set; }

    [ForeignKey("CardId")]
    public Card? Card { get; set; }
}