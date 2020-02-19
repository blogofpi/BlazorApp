using System;
using System.Collections.Generic;

namespace BlazorDataService.Models
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
    }
}
