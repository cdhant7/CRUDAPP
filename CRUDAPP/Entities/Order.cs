using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDAPP.Entities
{
    public class Order
    {
        public int orderid { get; set; }

        public string status { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public Store Store { get; internal set; }
        public int StoreId { get; set; }
    }
}
