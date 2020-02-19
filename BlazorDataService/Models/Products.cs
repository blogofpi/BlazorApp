using System;
using System.Collections.Generic;

namespace BlazorDataService.Models
{
    public partial class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public virtual Categories Category { get; set; }
    }
}
