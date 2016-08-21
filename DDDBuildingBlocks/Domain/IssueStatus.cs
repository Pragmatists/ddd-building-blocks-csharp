using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBuildingBlocks.Domain
{

    class IssueStatus : ValueObject<string>
    {
        protected IssueStatus() : base(null) { }
        protected IssueStatus(string value) : base(value) { }

        public static IssueStatus NEW = new IssueStatus("NEW");
        public static IssueStatus CLOSED = new IssueStatus("CLOSED");
        public static IssueStatus ASSIGNED = new IssueStatus("ASSIGNED");

    }
}