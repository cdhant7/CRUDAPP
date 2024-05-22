using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace CRUDAPP.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public int CompanyID { get; set; }
    }
}
