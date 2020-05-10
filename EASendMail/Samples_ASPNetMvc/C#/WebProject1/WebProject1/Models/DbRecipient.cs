using System.ComponentModel.DataAnnotations;

namespace WebProject1.Models
{
    public class DbRecipient
    {
        [Key, ScaffoldColumn(false)]
        public int RecipientId { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [Required, MaxLength(250)]
        public string Address { get; set; }
    }
}