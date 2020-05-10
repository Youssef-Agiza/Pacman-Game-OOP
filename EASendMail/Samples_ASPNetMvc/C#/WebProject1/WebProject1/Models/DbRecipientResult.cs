using System;
using System.ComponentModel.DataAnnotations;
namespace WebProject1.Models
{
    public class DbRecipientResult
    {
        [Key, ScaffoldColumn(false), Display(Name ="Result Id")]
        public int ResultId { get; set; }

        [Required, Display(Name ="Task Id")]
        public int TaskId { get; set; }

        [MaxLength(250)]
        public string Recipient { get; set; }

        public string Status { get; set; }

        [Display(Name = "Succeeded")]
        public bool IsSucceeded { get; set; }

        [Display(Name = "Date Time")]
        public DateTime CreationTime { get; set; }
    }
}