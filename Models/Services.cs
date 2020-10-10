using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteService.Models
{
    public partial class Services
    {
        [Key]
        [Column("service_id")]
        public int ServiceId { get; set; }
        [Column("type_id")]
        public int? TypeId { get; set; }
        [Required]
        [Column("service_name")]
        [StringLength(75)]
        public string ServiceName { get; set; }
        [Required]
        [Column("description")]
        [StringLength(250)]
        public string Description { get; set; }
        [Column("price", TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
        [Column("date_created", TypeName = "datetime")]
        public DateTime DateCreated { get; set; }

        [ForeignKey(nameof(TypeId))]
        [InverseProperty(nameof(ServiceTypes.Services))]
        public virtual ServiceTypes Type { get; set; }
    }
}
