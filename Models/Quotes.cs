using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteService.Models
{
    public partial class Quotes
    {
        [Key]
        [Column("quote_id")]
        public int QuoteId { get; set; }
        [Column("inquirer_id")]
        public int? InquirerId { get; set; }
        [Required]
        [Column("quote_estimate")]
        [StringLength(50)]
        public string QuoteEstimate { get; set; }
        [Column("expiration_date", TypeName = "datetime")]
        public DateTime ExpirationDate { get; set; }
        [Column("date_created", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(InquirerId))]
        [InverseProperty(nameof(Inquirers.Quotes))]
        public virtual Inquirers Inquirer { get; set; }
    }
}
