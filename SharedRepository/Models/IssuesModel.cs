using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedRepository.Models
{
   public class IssuesModel
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string IssueStatus { get; set; }
        public DateTime Completed { get; set; }
    }
}
