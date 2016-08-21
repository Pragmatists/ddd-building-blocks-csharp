using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBuildingBlocks.Domain
{
    class Issue
    {

        protected Issue()
        {
            this.Status = IssueStatus.NEW;
            this.RefferedIssues = new HashSet<IssueNumber>();
        }

        public Issue(IssueNumber number) : this()
        {
            this.Id = number;
        }

        public IssueNumber Id { get; protected set; }
        public string Title { get; protected set; }
        public IssueStatus Status { get; protected set; }

        protected ISet<IssueNumber> RefferedIssues { get; set; }

        public void RenameTo(String newTitle)
        {
            this.Title = newTitle;
        }

        public void ReffersTo(IssueNumber otherIssue)
        {
            RefferedIssues.Add(otherIssue);
        }
        public bool IsRefferingTo(IssueNumber otherIssue)
        {
            return RefferedIssues.Contains(otherIssue);
        }
    }

}
