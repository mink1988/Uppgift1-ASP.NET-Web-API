using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class Issue
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Customer { get; set; }

        [Required]
        [Column(TypeName =("nvarchar(max)"))]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime Completed { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string IssueStatus { get; set; }

        public int UserId { get; set; }
        




    }
}
