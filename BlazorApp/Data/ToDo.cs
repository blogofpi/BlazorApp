using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Data
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Task name is required")]
        [StringLength(15, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Due Date is required")]
        public DateTime DueDate { get; set; }
    }
}