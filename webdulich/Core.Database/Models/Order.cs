using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Database.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("CustomerId")]
        public Guid? CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public ICollection<Details> Details { get; set; } = new HashSet<Details>();
    }
}