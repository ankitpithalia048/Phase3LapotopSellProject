using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class LaptopModel
    {
        [Key]
        public string ID { get; set; }
        public string Product_Name { get; set; }
        public string Img_Url { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
