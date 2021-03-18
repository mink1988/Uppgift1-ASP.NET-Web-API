using System;

namespace SharedRepository.Models
{
    public class CreateIssueModel
    {
        public string Customer { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string IssueStatus { get; set; }
        public DateTime Completed { get; set; }
    }
}
