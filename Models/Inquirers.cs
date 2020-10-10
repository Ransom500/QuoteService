using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteService.Models
{
    public partial class Inquirers
    {
        public Inquirers()
        {
            Quotes = new HashSet<Quotes>();
        }

        [Key]
        [Column("inquirer_id")]
        public int InquirerId { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [Column("phone_number")]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [Required]
        [Column("email_address")]
        [StringLength(70)]
        public string EmailAddress { get; set; }
        [Column("date_created", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [InverseProperty("Inquirer")]
        public virtual ICollection<Quotes> Quotes { get; set; }
    }
}
