using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDAPP.Entities
{
    public class Employee
    {
        public int id { get; set; }

        public string firstname { get; set; }
        public string lastname { get; set; }
        public int homenumber { get; set; }
        public string cellphone { get; set; }
 
    }
}
