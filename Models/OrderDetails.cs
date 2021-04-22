using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    
    
    public class OrderDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Oid { get; set; }
        [NotMapped]
        public LaptopModel Product { get; set; }
        public string Pid { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string  Phone { get; set; }
    }
}
