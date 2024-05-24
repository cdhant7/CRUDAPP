using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDAPP.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }
    }
}
