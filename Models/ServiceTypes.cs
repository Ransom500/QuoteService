using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteService.Models
{
    [Table("Service_Types")]
    public partial class ServiceTypes
    {
        public ServiceTypes()
        {
            Services = new HashSet<Services>();
        }

        [Key]
        [Column("type_id")]
        public int TypeId { get; set; }
        [Required]
        [Column("title")]
        [StringLength(30)]
        public string Title { get; set; }
        [Column("date_created", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<Services> Services { get; set; }
    }
}
